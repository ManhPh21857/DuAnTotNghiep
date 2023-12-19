namespace Project.Sales.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
    }
}
