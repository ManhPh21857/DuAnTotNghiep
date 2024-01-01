namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get
{
    public class SupplierResponseModel
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        public SupplierResponseModel()
        {
            Suppliers = new List<SupplierModel>();
        }
    }
}
