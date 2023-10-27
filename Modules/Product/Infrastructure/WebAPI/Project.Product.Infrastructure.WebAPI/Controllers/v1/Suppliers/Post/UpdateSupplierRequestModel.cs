using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post
{
    public class UpdateSupplierRequestModel
    {
        public IEnumerable<UpdateSupplierModel> Suppliers { get; set; }

        public UpdateSupplierRequestModel()
        {
            Suppliers = new List<UpdateSupplierModel>();
        }
    }

    public class UpdateSupplierRequestModelValidator : AbstractValidator<UpdateSupplierRequestModel>
    {
        public UpdateSupplierRequestModelValidator(IValidator<UpdateSupplierModel> updateSupplierModelValidator)
        {
            this.RuleForEach(x => x.Suppliers).SetValidator(updateSupplierModelValidator);
        }
    }
}
