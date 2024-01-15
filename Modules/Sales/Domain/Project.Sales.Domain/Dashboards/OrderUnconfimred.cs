namespace Project.Sales.Domain.Dashboards
{
    public class OrderUnconfimred
    {
        public Guid OrderCode { get; set; }
        public string FullName { get; set; }
        public float OrderTotal { get; set; }
        public int Status { get; set; }
    }
}
