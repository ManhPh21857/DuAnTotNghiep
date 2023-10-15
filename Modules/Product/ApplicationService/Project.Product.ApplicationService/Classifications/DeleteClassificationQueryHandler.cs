using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Classifications
{
    public class DeleteClassificationQueryHandler : CommandHandler<DeleteClassificationQuery, DeleteClassificationQueryResult>
    {
        private readonly IClassificationRepository classification;
        public DeleteClassificationQueryHandler(IClassificationRepository classification)
        {
            this.classification = classification;
        }
        public async override Task<DeleteClassificationQueryResult> Handle(DeleteClassificationQuery request, CancellationToken cancellationToken)
        {
            var delete = new ClassificationInfo();
            delete.Id = request.Id;
     
            await classification.DeleteClassifications(delete);
            return new DeleteClassificationQueryResult(true);
        }
    }
}
