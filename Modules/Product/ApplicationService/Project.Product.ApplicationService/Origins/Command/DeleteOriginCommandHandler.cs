using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var delete = new OriginInfo()
            {
                Id = request.Id
            };
            await Origin.DeleteOrigin(delete);
            return new DeleteOriginCommandResult(true);
        }
    }
}
