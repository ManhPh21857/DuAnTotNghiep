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
    public class UpdateOriginQueryHandler : CommandHandler<UpdateOriginQuery, UpdateOriginQueryResult>
    {
        private readonly IOriginRepository Origin;
        public UpdateOriginQueryHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<UpdateOriginQueryResult> Handle(UpdateOriginQuery request, CancellationToken cancellationToken)
        {
            var update = new OriginInfo()
            {
                Id = request.Id,
                Name = request.Name
            };
            await Origin.UpdateOrigin(update);
            return new UpdateOriginQueryResult(true);
        }
    }
}
