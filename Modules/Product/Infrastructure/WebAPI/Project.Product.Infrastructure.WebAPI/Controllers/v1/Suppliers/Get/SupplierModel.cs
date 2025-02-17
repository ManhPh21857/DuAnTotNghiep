﻿namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get
{
    public class SupplierModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
