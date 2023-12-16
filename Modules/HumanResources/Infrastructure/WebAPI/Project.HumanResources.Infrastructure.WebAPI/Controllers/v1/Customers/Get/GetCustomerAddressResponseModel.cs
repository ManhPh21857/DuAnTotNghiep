namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Get
{
    public class GetCustomerAddressResponseModel
    {
        public IEnumerable<CustomerAddressModel> CustomerAddresses { get; set; }

        public GetCustomerAddressResponseModel()
        {
            this.CustomerAddresses = new List<CustomerAddressModel>();
        }
    }
}
