using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post
{
    public class UpdateClassificationsRequestModel
    {
        public IEnumerable<UpdateClassificationsModel> Classifications { get; set; }

        public UpdateClassificationsRequestModel()
        {
            Classifications = new List<UpdateClassificationsModel>();
        }
    }
    public class UpdateClassificationsRequestModelValidator : AbstractValidator<UpdateClassificationsRequestModel>
    {
        public UpdateClassificationsRequestModelValidator(IValidator<UpdateClassificationsModel> updateClassificationsModelValidator)
        {
            this.RuleForEach(x => x.Classifications).SetValidator(updateClassificationsModelValidator);
        }
    }
}
