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
    public class CreateOriginCommandHandler : CommandHandler<CreateOriginCommand, CreateOriginCommandResult>
    {
        private readonly IOriginRepository Origin;
        public CreateOriginCommandHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<CreateOriginCommandResult> Handle(CreateOriginCommand request, CancellationToken cancellationToken)
        {
            var create = new OriginInfo()
            {
                Name = request.Name
            }; 
            var check = await Origin.CheckOriginName(request.Name);
            if (check is not null)
            {
                throw new Exception();
            }
            await Origin.CreateOrigin(create);
            return new CreateOriginCommandResult(true);
        }
    }
}
