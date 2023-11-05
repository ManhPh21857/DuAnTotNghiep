
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications.Command;

namespace Project.Product.ApplicationService.Classifications.Command
{
    public class ReactiveClassificationCommandHandler : CommandHandler<ReactiveClassificationCommand, ReactiveClassificationCommandResult>
    {
        private readonly IClassificationRepository classificationRepository;

        public ReactiveClassificationCommandHandler(IClassificationRepository classificationRepository)
        {
            this.classificationRepository = classificationRepository;
        }
        public async override Task<ReactiveClassificationCommandResult> Handle(ReactiveClassificationCommand request, CancellationToken cancellationToken)
        {
            var param = new ClassificationInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.classificationRepository.ReActiveClassification(param);

            return new ReactiveClassificationCommandResult(true);
        }


    }
}
