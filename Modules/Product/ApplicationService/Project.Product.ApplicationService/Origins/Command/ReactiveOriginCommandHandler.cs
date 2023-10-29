
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Command;

namespace Project.Product.ApplicationService.Origins.Command
{
    public class ReactiveOriginCommandHandler : CommandHandler<ReactiveOriginCommand, ReactiveOriginCommandResult>
    {
        private readonly IOriginRepository originRepository;

        public ReactiveOriginCommandHandler(IOriginRepository originRepository)
        {
            this.originRepository = originRepository;
        }
     
        public async override Task<ReactiveOriginCommandResult> Handle(ReactiveOriginCommand request, CancellationToken cancellationToken)
        {
            var param = new OriginInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.originRepository.ReactiveOrigin(param);

            return new ReactiveOriginCommandResult(true);
        }
    }
}
