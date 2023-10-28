using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Classifications.Command
{
    public class UpdateClassificationCommandHandler : CommandHandler<UpdateClassificationCommand, UpdateClassificationCommandResult>
    {
        private readonly IClassificationRepository classification;
        public UpdateClassificationCommandHandler(IClassificationRepository classification)
        {
            this.classification = classification;

        }

        public override async Task<UpdateClassificationCommandResult> Handle(UpdateClassificationCommand request, CancellationToken cancellationToken)
        {
            var update = new ClassificationInfo();
            update.Id = request.Id;
            update.Name = request.Name;
            await classification.UpdateClassifications(update);
            return new UpdateClassificationCommandResult(true);
        }
    }
}
