using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Put
{
    public class DeleteEmployeeRequestModel
    {
        public IEnumerable<DeleteEmployeeModel> Employees { get; set; }

        public DeleteEmployeeRequestModel()
        {
            this.Employees = new List<DeleteEmployeeModel>();
        }
    }

    public class DeleteEmployeeRequestModelValidator : AbstractValidator<DeleteEmployeeRequestModel>
    {
        public DeleteEmployeeRequestModelValidator(IValidator<DeleteEmployeeModel> validator)
        {
            this.RuleForEach(x => x.Employees).SetValidator(validator);
        }
    }
}
