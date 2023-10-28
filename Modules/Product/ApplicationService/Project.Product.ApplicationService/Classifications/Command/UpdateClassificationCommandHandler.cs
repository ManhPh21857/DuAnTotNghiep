using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications.Command;

namespace Project.Product.ApplicationService.Classifications.Command
{
    public class UpdateClassificationCommandHandler : CommandHandler<UpdateClassificationCommand, UpdateClassificationCommandResult>
    {
        private readonly IClassificationRepository classificationRepository;

        public UpdateClassificationCommandHandler(IClassificationRepository classificationRepository)
        {
            this.classificationRepository = classificationRepository;
        }
        public async override Task<UpdateClassificationCommandResult> Handle(UpdateClassificationCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Classifications)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    await this.classificationRepository.CreateClassification(item);
                }
                else
                {
                    await this.classificationRepository.UpdateClassification(item);
                }
            }
            return new UpdateClassificationCommandResult(true);
        }
    }
}
