using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Colors;
using Project.Product.Integration.Colors.Command;

namespace Project.Product.ApplicationService.Colors.Command
{
    public class ReactiveColorCommandHandler : CommandHandler<ReactiveColorCommand, ReactiveColorCommandResult>
    {
        private readonly IColorRepository colorRepository;

        public ReactiveColorCommandHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async override Task<ReactiveColorCommandResult> Handle(ReactiveColorCommand request, CancellationToken cancellationToken)
        {
            var param = new ColorInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.colorRepository.ReActiveColor(param);

            return new ReactiveColorCommandResult(true);
        }
    }
}
