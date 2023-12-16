using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Vouchers.Query;

namespace Project.Sales.ApplicationService.Vouchers.Query
{
    public class GetVoucherQueryHandler : QueryHandler<GetVoucherQuery, GetVoucherQueryResult>
    {
        private readonly IVoucherRepository voucherRepository;

        public GetVoucherQueryHandler(IVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
        }

        public async override Task<GetVoucherQueryResult> Handle(
            GetVoucherQuery request,
            CancellationToken cancellationToken
        )
        {
            var vouchers = await this.voucherRepository.GetVoucher(request.TotalPrice);

            return new GetVoucherQueryResult(vouchers);
        }
    }
}
