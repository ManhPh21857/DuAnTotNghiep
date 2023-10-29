using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Sizes;
using Project.Product.Integration.Sizes.Command;

namespace Project.Product.ApplicationService.Sizes.Command
{
    public class DeleteSizeCommandHandler : CommandHandler<DeleteSizeCommand, DeleteSizeCommandResult>
    {
        private readonly ISizeRepository sizeRepository;

        public DeleteSizeCommandHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async override Task<DeleteSizeCommandResult> Handle(
            DeleteSizeCommand request,
            CancellationToken cancellationToken
        )
        {
            var param = new SizeInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.sizeRepository.DeleteSize(param);

            return new DeleteSizeCommandResult(true);
        }
    }
}
