using Project.Core.ApplicationService.Queries;
using Project.Core.Domain.Constants;
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
            IEnumerable<Voucher> vouchers;
            int totalPage = 0;
            if (request.TotalPrice.HasValue)
            {
                vouchers = await this.voucherRepository.GetVoucher(request.TotalPrice.Value);
            }
            else
            {
                vouchers = await this.GetAllVoucher(request.Page ?? 1);

                var total = await this.voucherRepository.GetTotalPage();
                totalPage = total / CommonConst.VOUCHER_PAGE_SIZE;
                if (total % CommonConst.VOUCHER_PAGE_SIZE > 0)
                {
                    totalPage++;
                }
            }



            return new GetVoucherQueryResult(vouchers, totalPage);
        }

        private async Task<IEnumerable<Voucher>> GetAllVoucher(int page)
        {
            int skip = (page - 1) * CommonConst.VOUCHER_PAGE_SIZE;
            int take = CommonConst.VOUCHER_PAGE_SIZE;

            return await this.voucherRepository.GetAllVoucher(skip, take);
        }
    }
}
