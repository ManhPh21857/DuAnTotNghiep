using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Users.Command;

namespace Project.HumanResources.ApplicationService.Users.Command
{
    public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand, ChangePasswordCommandResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionInfo sessionInfo;

        public ChangePasswordCommandHandler(IUserRepository userRepository, ISessionInfo sessionInfo)
        {
            this.userRepository = userRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<ChangePasswordCommandResult> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.Value;
            var user = await this.userRepository.CheckUserExistence(userId);
            if (request.OldPassword != user.Password)
            {
                throw new DomainException("", "Mật khẩu không chính xác");
            }

            await this.userRepository.ChangePassword(userId, request.OldPassword, request.NewPassword);

            return new ChangePasswordCommandResult(true);
        }
    }
}
