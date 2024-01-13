using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Command;
using SixLabors.ImageSharp;

namespace Project.HumanResources.ApplicationService.Employees.Command
{
    public class UpdateEmployeeCommandHandler : CommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IConfiguration configuration;

        public UpdateEmployeeCommandHandler(
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IConfiguration configuration
        )
        {
            this.userRepository = userRepository;
            this.employeeRepository = employeeRepository;
            this.configuration = configuration;
        }

        public async override Task<UpdateEmployeeCommandResult> Handle(
            UpdateEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            if (request.EmployeeDataVersion.IsNullOrEmpty())
            {
                var user = await this.userRepository.GetUserRegister(request.Email, request.Username);
                if (!user.IsNullOrEmpty())
                {
                    var exception = new DomainException("", "tài khoản đã tồn tại");

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
                await image.SaveAsync(@$"{this.configuration["ImagePath"]}{userImage}", CancellationToken.None);

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
            }
            else
            {
                string userImage;

                if (this.IsBase64String(request.Image))
                {
                    userImage = $"{request.Username}_avatar.png";
                    var imageBytes = Convert.FromBase64String(request.Image);
                    using var image = Image.Load(imageBytes);
                    await image.SaveAsync(@$"{this.configuration["ImagePath"]}{userImage}", CancellationToken.None);
                }
                else
                {
                    userImage = request.Image;
                }

                await this.employeeRepository.UpdateEmployee(new UpdateEmployeeParam
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PhoneNumber = request.PhoneNumber,
                        Sex = request.Sex,
                        Birthday = request.Birthday,
                        Image = userImage,
                        Address = request.Address,
                        Id = request.Id,
                        DataVersion = request.EmployeeDataVersion
                    }
                );

                await this.userRepository.UpdateUser(new UpdateUserParam
                    {
                        Id = request.UserId,
                        Password = request.Password,
                        DataVersion = request.UserDataVersion
                    }
                );

                await this.userRepository.DeleteUserRoles(request.UserId);

                foreach (int role in request.Roles)
                {
                    await this.userRepository.InsertUserRole(new InsertUserRoleParam { UserId = request.UserId, Role = role });
                }


            }
            
            scope.Complete();

            return new UpdateEmployeeCommandResult(true);
        }

        public bool IsBase64String(string base64)
        {
            var buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int _);
        }
    }
}
