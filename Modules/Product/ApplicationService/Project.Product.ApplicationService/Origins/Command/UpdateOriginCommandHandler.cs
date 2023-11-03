using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Command;

namespace Project.Product.ApplicationService.Origins.Command
{
    public class UpdateOriginCommandHandler : CommandHandler<UpdateOriginCommand, UpdateOriginCommandResult>
    {
        private readonly IOriginRepository Origin;
        public UpdateOriginCommandHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<UpdateOriginCommandResult> Handle(UpdateOriginCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Origins)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var check = await Origin.CheckOriginName(item.Name);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.Origin.CreateOrigin(item);
                }
                else
                {
                    var check = await Origin.CheckOriginName(item.Name);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.Origin.UpdateOrigin(item);
                }
            }

            return new UpdateOriginCommandResult(true);
        }
    }
}
