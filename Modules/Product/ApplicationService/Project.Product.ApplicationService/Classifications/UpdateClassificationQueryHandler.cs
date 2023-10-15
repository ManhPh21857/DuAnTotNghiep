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
    public class UpdateClassificationQueryHandler : CommandHandler<UpdateClassificationQuery, UpdateClassificationQueryResult>
    {
        private readonly IClassificationRepository classification;
        public UpdateClassificationQueryHandler(IClassificationRepository classification)
        {
            this.classification = classification;

        }

        public override async Task<UpdateClassificationQueryResult> Handle(UpdateClassificationQuery request, CancellationToken cancellationToken)
        {
            var update = new ClassificationInfo();
            update.Id = request.Id;
            update.Name = request.Name;
            await classification.UpdateClassifications(update);
            return new UpdateClassificationQueryResult(true);
        }
    }
}
