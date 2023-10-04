using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.Register;

namespace Project.HumanResources.ApplicationService.Authentication;

public class RegisterServices : CommandHandler<RegisterRequest, RegisterResponse>
{
    private readonly IUserRepository userRepository;

    public RegisterServices(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async override Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        using var scope = TransactionFactory.Create();

        var getUserParam = new GetUserParam
        {
            Username = request.UserName
        };

        var user = await userRepository.GetUser(getUserParam);
        if (user is not null)
        {
            var exception = new DomainException("", "Username already exists");

            throw exception;
        }

        var generator = new Random();
        int r = generator.Next(1, 999999);
        string code = r.ToString().PadLeft(6, '0');

        string uid = request.IsEmployee ? $"VE{code}" : $"VC{code}";

        var registerUserParam = new RegisterUserParam
        {
            UID = uid,
            Username = request.UserName,
            Password = request.Password
        };

        int userId = await userRepository.RegisterUser(registerUserParam);

        if (!request.Roles.IsNullOrEmpty())
        {
            foreach (var role in request.Roles)
            {
                await userRepository.InsertUserRole(new InsertUserRoleParam { UserId = userId, Role = role });
            }
        }

        var insertUserInfoParam = new InsertUserInfoParam
        {
            UserId = userId,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = ""
        };

        await userRepository.InsertUserInfo(insertUserInfoParam);

        scope.Complete();

        return new RegisterResponse(true);
    }
}