using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Colors;
using Project.Product.Integration.Colors.Command;

namespace Project.Product.ApplicationService.Colors.Command
{
    public class DeleteColorCommandHandler : CommandHandler<DeleteColorCommand, DeleteColorCommandResult>
    {
        private readonly IColorRepository colorRepository;

        public DeleteColorCommandHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public async override Task<DeleteColorCommandResult> Handle(
            DeleteColorCommand request,
            CancellationToken cancellationToken
        )
        {
            var param = new ColorInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.colorRepository.DeleteColor(param);

            return new DeleteColorCommandResult(true);
        }
    }
}