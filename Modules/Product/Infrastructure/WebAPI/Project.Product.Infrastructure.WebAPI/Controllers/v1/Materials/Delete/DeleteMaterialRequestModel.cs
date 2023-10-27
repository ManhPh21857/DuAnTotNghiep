using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Delete
{
    public class DeleteMaterialRequestModel
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
