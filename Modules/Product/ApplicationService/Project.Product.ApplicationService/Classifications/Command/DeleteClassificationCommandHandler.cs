using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications.Command;

namespace Project.Product.ApplicationService.Classifications.Command
{
    public class DeleteClassificationCommandHandler : CommandHandler<DeleteClassificationCommand, DeleteClassificationCommandResult>
    {
        private readonly IClassificationRepository classificationRepository;
        public DeleteClassificationCommandHandler(IClassificationRepository classificationRepository)
        {
            this.classificationRepository = classificationRepository;
        }
        public override async Task<DeleteClassificationCommandResult> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken)
        {
            var param = new ClassificationInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.classificationRepository.DeleteClassification(param);

            return new DeleteClassificationCommandResult(true);
        }
    }
}
