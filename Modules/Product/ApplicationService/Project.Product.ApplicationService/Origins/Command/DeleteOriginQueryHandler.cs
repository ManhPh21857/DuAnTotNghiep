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
    public class DeleteOriginQueryHandler : CommandHandler<DeleteOriginQuery, DeleteOriginQueryResult>
    {
        private readonly IOriginRepository Origin;
        public DeleteOriginQueryHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<DeleteOriginQueryResult> Handle(DeleteOriginQuery request, CancellationToken cancellationToken)
        {
            var delete = new OriginInfo()
            {
                Id = request.Id
            };
            await Origin.DeleteOrigin(delete);
            return new DeleteOriginQueryResult(true);
        }
    }
}
