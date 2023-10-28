using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post
{
    public class UpdateOriginRequestModel
    {
        public IEnumerable<UpdateOriginModel> Origins { get; set; }

        public UpdateOriginRequestModel()
        {
            Origins = new List<UpdateOriginModel>();
        }
    }

    public class UpdateOriginRequestModelValidator : AbstractValidator<UpdateOriginRequestModel>
    {
        public UpdateOriginRequestModelValidator(IValidator<UpdateOriginModel> updateOriginModelValidator)
        {
            this.RuleForEach(x => x.Origins).SetValidator(updateOriginModelValidator);
        }
    }
}
