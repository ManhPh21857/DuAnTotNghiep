namespace Project.Sales.Domain.Vouchers
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetVoucher(float totalPrice);
    }
}
