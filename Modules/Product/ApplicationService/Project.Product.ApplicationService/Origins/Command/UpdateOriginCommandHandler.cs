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
    public class UpdateOriginCommandHandler : CommandHandler<UpdateOriginQuery, UpdateOriginCommandResult>
    {
        private readonly IOriginRepository Origin;
        public UpdateOriginCommandHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<UpdateOriginCommandResult> Handle(UpdateOriginQuery request, CancellationToken cancellationToken)
        {
            var update = new OriginInfo()
            {
                Id = request.Id,
                Name = request.Name
            };
            var check = await Origin.CheckOriginName(request.Name);
            if (check is not null)
            {
                throw new Exception();
            }
            await Origin.UpdateOrigin(update);
            return new UpdateOriginCommandResult(true);
        }
    }
}
