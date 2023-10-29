using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post
{
	public class UpdateMaterialRequestModel
	{
		public IEnumerable<UpdateMaterialModel> Materials { get; set; }

		public UpdateMaterialRequestModel()
		{
			Materials = new List<UpdateMaterialModel>();
		}
       
	}

	public class UpdateMaterialRequestModelValidator : AbstractValidator<UpdateMaterialRequestModel>
	{
		public UpdateMaterialRequestModelValidator(IValidator<UpdateMaterialModel> updateMaterialModelValidator)
		{
			this.RuleForEach(x => x.Materials).SetValidator(updateMaterialModelValidator);
		}
	}
}
