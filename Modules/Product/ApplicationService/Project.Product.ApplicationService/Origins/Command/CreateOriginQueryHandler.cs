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
    public class CreateOriginQueryHandler : CommandHandler<CreateOriginQuery, CreateOriginQueryResult>
    {
        private readonly IOriginRepository Origin;
        public CreateOriginQueryHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<CreateOriginQueryResult> Handle(CreateOriginQuery request, CancellationToken cancellationToken)
        {
            var create = new OriginInfo()
            {
                Name = request.Name
            };
            await Origin.CreateOrigin(create);
            return new CreateOriginQueryResult(true);
        }
    }
}
