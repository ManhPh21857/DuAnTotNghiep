using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Product.Domain.Colors;
using Project.Product.Integration.Colors.Command;

namespace Project.Product.ApplicationService.Colors.Command
{
    public class UpdateColorCommandHandler : CommandHandler<UpdateColorCommand, UpdateColorCommandResult>
    {
        private readonly IColorRepository colorRepository;

        private readonly ISessionInfo sessionInfo;

        public UpdateColorCommandHandler(IColorRepository colorRepository, ISessionInfo sessionInfo)
        {
            this.colorRepository = colorRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateColorCommandResult> Handle(
            UpdateColorCommand request,
            CancellationToken cancellationToken
        )
        {
            var userId = sessionInfo.UserId;
            var sessionId = sessionInfo.SessionId;


            foreach (var item in request.Colors)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    await this.colorRepository.CreateColor(item);
                }
                else
                {
                    await this.colorRepository.UpdateColor(item);
                }
            }

            return new UpdateColorCommandResult(true);
        }
    }
}