using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.Login;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Project.Core.Domain.Enums;
using Project.Core.Domain.User;

namespace Project.HumanResources.ApplicationService.Authentication;

public class LoginService : CommandHandler<LoginRequest, LoginResponse>
{
    private readonly IConfiguration configuration;
    private readonly IUserRepository userRepository;

    public LoginService(
        IConfiguration configuration,
        IUserRepository userRepository
    )
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
    }

    public async override Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var getUserParam = new GetUserParam
        {
            Username = request.UserName,
            Password = request.Password
        };

        var user = (await this.userRepository.GetUserLogin(getUserParam)).SingleOrDefault();
        if (user is null)
        {
            var exception = new DomainException("", "tài khoản hoặc mật khẩu không chính xác");

            throw exception;
        }

        if (user.IsDeleted == 1)
        {
            var exception = new DomainException("", "tài khoản đã bị xóa");

            throw exception;
        }

        var roles = await this.userRepository.GetUserRoles(user.Id);
        if (roles.All(x => x.Id != Role.ShopLogin.GetHashCode()))
        {
            var exception = new DomainException("", "tài khoản hoặc mật khẩu không chính xác");

            throw exception;
        }

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
        var token = new JwtSecurityToken(
            this.configuration["Jwt:Issuer"],
            this.configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        var bearToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse(bearToken);
    }
}
