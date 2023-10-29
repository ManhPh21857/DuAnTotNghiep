using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Sizes;
using Project.Product.Integration.Sizes.Command;

namespace Project.Product.ApplicationService.Sizes.Command
{
    public class ReactiveSizeCommandHandler : CommandHandler<ReactiveSizeCommand, ReactiveSizeCommandResult>
    {
        private readonly ISizeRepository sizeRepository;

        public ReactiveSizeCommandHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }
        public async override Task<ReactiveSizeCommandResult> Handle(ReactiveSizeCommand request, CancellationToken cancellationToken)
        {
            var param = new SizeInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.sizeRepository.ReActiveSize(param);

            return new ReactiveSizeCommandResult(true);
        }
    }
}
