using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain.User;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Login;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Project.Core.Domain.Enums;

namespace Project.HumanResources.ApplicationService.Employees
{
    public class EmployeeLoginService : CommandHandler<EmployeeLoginRequest, EmployeeLoginResponse>
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public EmployeeLoginService(
            IConfiguration configuration,
            IUserRepository userRepository
        )
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public async override Task<EmployeeLoginResponse> Handle(
            EmployeeLoginRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = (await this.userRepository.GetEmployeeLogin(request.UserName,
                request.Password,
                Role.ManagementLogin.GetHashCode())).SingleOrDefault();
            if (user is null)
            {
                var exception = new DomainException("", "username or password is incorrect");

                throw exception;
            }

            if (user.IsDeleted == 1)
            {
                var exception = new DomainException("", "account has been deleted");

                throw exception;
            }

            var roles = await this.userRepository.GetUserRoles(user.Id);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, this.configuration["Jwt:Subject"]),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)),
                new($"{nameof(UserId).ToLower()}", user.Id.ToString()),
                new("Username", user.Username),
            };

            claims.AddRange(roles.Select(role =>
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), role.Id) ?? "")));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            var bearToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new EmployeeLoginResponse(bearToken);
        }
    }
}
