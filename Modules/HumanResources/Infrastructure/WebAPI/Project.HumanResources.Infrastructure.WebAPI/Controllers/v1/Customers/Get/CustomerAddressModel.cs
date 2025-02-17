﻿namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Get
{
    public class CustomerAddressModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string AddressDetail { get; set; }
        public int IsDefault { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
