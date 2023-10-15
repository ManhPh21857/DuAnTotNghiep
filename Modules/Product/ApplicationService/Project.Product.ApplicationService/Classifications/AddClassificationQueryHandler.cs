﻿using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications;
namespace Project.Product.ApplicationService.Classifications
{
    public class AddClassificationQueryHandler : CommandHandler<AddClassificationQuery, AddClassificationQueryResult>
    {  
        private readonly IClassificationRepository classification;

        public AddClassificationQueryHandler(IClassificationRepository classification)
        {
            this.classification = classification;

        }
        public override async Task<AddClassificationQueryResult> Handle(AddClassificationQuery request, CancellationToken cancellationToken)
        {

            using var scope = TransactionFactory.Create();
            var addclassification = new ClassificationInfo
            {
                Name = request.Name,
            };

            await classification.AddClassifications(addclassification);

            scope.Complete();

            return new AddClassificationQueryResult(true);
        }
    }
}
