using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.Register;

namespace Project.HumanResources.ApplicationService.Authentication;

public class RegisterServices : CommandHandler<RegisterRequest, RegisterResponse>
{
    private readonly IUserRepository userRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IMemoryCache memoryCache;
    private readonly IRoleRepository roleRepository;

    public RegisterServices(
        IUserRepository userRepository,
        ICustomerRepository customerRepository,
        IMemoryCache memoryCache,
        IRoleRepository roleRepository
    )
    {
        this.userRepository = userRepository;
        this.customerRepository = customerRepository;
        this.memoryCache = memoryCache;
        this.roleRepository = roleRepository;
    }

    public async override Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        using var scope = TransactionFactory.Create();

        this.memoryCache.TryGetValue(request.Email, out string value);

        if (value is null || request.Code != value)
        {
            throw new DomainException(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                nameof(HttpStatusCode.BadRequest));
        }

        var user = await this.userRepository.GetUserRegister(request.Email, request.UserName);
        if (!user.IsNullOrEmpty())
        {
            var exception = new DomainException("", "Username already exists");

            throw exception;
        }

        var generator = new Random();
        int r = generator.Next(1, 999999);
        string code = r.ToString().PadLeft(6, '0');

        string uid = $"VC{code}";

        var registerUserParam = new RegisterUserParam
        {
            Email = request.Email,
            Username = request.UserName,
            Password = request.Password
        };

        int userId = await this.userRepository.RegisterUser(registerUserParam);

        var roles = await this.roleRepository.GetGroupRole(GroupRole.Customer.GetHashCode());

        foreach (int role in roles)
        {
            await this.userRepository.InsertUserRole(new InsertUserRoleParam { UserId = userId, Role = role });
        }

        await this.customerRepository.InsertCustomer(new InsertCustomerParam
            {
                UID = uid,
                UserId = userId
            }
        );

        scope.Complete();

        return new RegisterResponse(true);
    }
}
