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

        var user = (await userRepository.GetUser(getUserParam)).SingleOrDefault();
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

        var roles = await userRepository.GetUserRoles(user.Id);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)),
            new($"{nameof(UserId).ToLower()}", user.Id.ToString()),
            new("UID", user.UID),
            new("Username", user.Username),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.RoleName.ToString())));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        var bearToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse(bearToken);
    }
}