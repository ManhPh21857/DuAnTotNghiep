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
    public class DeleteClassificationQueryHandler : CommandHandler<DeleteClassificationCommand, DeleteClassificationCommandResult>
    {
        private readonly IClassificationRepository classification;
        public DeleteClassificationQueryHandler(IClassificationRepository classification)
        {
            this.classification = classification;
        }
        public async override Task<DeleteClassificationCommandResult> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken)
        {
            var delete = new ClassificationInfo();
            delete.Id = request.Id;

            await classification.DeleteClassifications(delete);
            return new DeleteClassificationCommandResult(true);
        }
    }
}
