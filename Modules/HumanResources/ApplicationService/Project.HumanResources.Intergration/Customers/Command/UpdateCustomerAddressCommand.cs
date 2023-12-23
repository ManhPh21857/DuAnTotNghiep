using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateCustomerAddressCommand : ICommand<UpdateCustomerAddressCommandResult>
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string AddressDetail { get; set; }
        public byte[]? DataVersion { get; set; }

        public UpdateCustomerAddressCommand(
            int? id,
            string customerName,
            string phoneNumber,
            string city,
            string district,
            string commune,
            string addressDetail,
            byte[]? dataVersion
        )
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.PhoneNumber = phoneNumber;
            this.City = city;
            this.District = district;
            this.Commune = commune;
            this.AddressDetail = addressDetail;
            this.DataVersion = dataVersion;
        }
    }
}
