namespace Project.Core.Domain.Enums
{
    public enum OrderStatus
    {
        NeedToConfirm = 0,
        Preparing = 1,
        Deliver = 2,
        Received = 3,
        Cancel = 4,
        Refund = 5
    }
}
