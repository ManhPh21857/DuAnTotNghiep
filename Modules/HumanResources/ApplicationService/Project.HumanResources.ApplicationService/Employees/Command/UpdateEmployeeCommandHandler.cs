using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Command;
using SixLabors.ImageSharp;

namespace Project.HumanResources.ApplicationService.Employees.Command
{
    public class UpdateEmployeeCommandHandler : CommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateEmployeeCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IEmployeeRepository employeeRepository,
            ISessionInfo sessionInfo
        )
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.employeeRepository = employeeRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateEmployeeCommandResult> Handle(
            UpdateEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            var user = await this.userRepository.GetUserRegister(request.Email, request.Username);
            if (!user.IsNullOrEmpty())
            {
                var exception = new DomainException("", "Username already exists");

                throw exception;
            }

            var generator = new Random();
            int r = generator.Next(1, 999999);
            string code = r.ToString().PadLeft(6, '0');

            string uid = $"VN{code}";

            var registerUserParam = new RegisterUserParam
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };

            int userId = await this.userRepository.RegisterUser(registerUserParam);

            var roles = request.Roles;

            foreach (int role in roles)
            {
                await this.userRepository.InsertUserRole(new InsertUserRoleParam { UserId = userId, Role = role });
            }

            var userImage = $"{request.Username}_avatar.png";

            var imageBytes = Convert.FromBase64String(request.Image);
            using var image = Image.Load(imageBytes);
            await image.SaveAsync(@$"D:\Final\Img\{userImage}", CancellationToken.None);

            await this.employeeRepository.CreateEmployee(new CreateEmployeeParam
                {
                    UID = uid,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Image = userImage,
                    Address = request.Address,
                    Birthday = request.Birthday,
                    Sex = request.Sex,
                    PhoneNumber = request.PhoneNumber,
                    UserId = userId
                }
            );

            scope.Complete();

            return new UpdateEmployeeCommandResult(true);
        }
    }
}
