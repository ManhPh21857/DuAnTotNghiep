namespace Project.Sales.Domain.Vouchers
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetVoucher(float totalPrice);
        Task<IEnumerable<Voucher>> GetAllVoucher(int skip, int take);
        Task<int> GetTotalPage();
        Task CreateVoucher(CreateVoucherParam param);
        Task UpdateVoucher(UpdateVoucherParam param);

        Task DeleteVoucher(
            int id,
            int then,
            int now,
            byte[]? dataVersion,
            int lastUpdateBy
        );
    }
}
