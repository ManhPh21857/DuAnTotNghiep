namespace Project.Core.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = -1,
        NeedToConfirm = 0,
        Preparing = 1,
        Deliver = 2,
        Received = 3,
        Cancel = 4,
        Refund = 5
    }
}
