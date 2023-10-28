using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.CartDetails.Query
{
    public class GetCartdetailQueryHandler : QueryHandler<GetCartdetailQuery, GetCartdetailQueryResult>
    {
        private readonly ICartdetailRepository cartService;

        public GetCartdetailQueryHandler(ICartdetailRepository cartService)
        {
            this.cartService = cartService;
        }

        public async override Task<GetCartdetailQueryResult> Handle(GetCartdetailQuery request, CancellationToken cancellationToken)
        {
            var result = await cartService.GetCartdetai();

            return new GetCartdetailQueryResult(result.ToList());
        }
    }
}
