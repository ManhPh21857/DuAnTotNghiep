using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Command;

namespace Project.Product.ApplicationService.Origins.Command
{
    public class DeleteOriginCommandHandler : CommandHandler<DeleteOriginCommand, DeleteOriginCommandResult>
    {
        private readonly IOriginRepository Origin;
        public DeleteOriginCommandHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<DeleteOriginCommandResult> Handle(DeleteOriginCommand request, CancellationToken cancellationToken)
        {
            var param = new OriginInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.Origin.DeleteOrigin(param);

            return new DeleteOriginCommandResult(true);
        }
    }
}
