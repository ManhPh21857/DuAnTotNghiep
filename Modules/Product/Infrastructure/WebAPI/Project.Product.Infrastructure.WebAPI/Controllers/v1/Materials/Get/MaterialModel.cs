﻿namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Get
{
    public class MaterialModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
