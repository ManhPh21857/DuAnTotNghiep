namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get
{
    public class SupplierViewResponseModel
    {
        public IEnumerable<SupplierViewModel> Suppliers { get; set; }

        public SupplierViewResponseModel()
        {
            this.Suppliers = new List<SupplierViewModel>();
        }
    }
}
