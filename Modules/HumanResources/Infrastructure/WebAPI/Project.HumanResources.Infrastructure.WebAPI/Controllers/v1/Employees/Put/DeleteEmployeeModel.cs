using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Put
{
    public class DeleteEmployeeModel
    {
        public int? Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class DeleteEmployeeModelValidator : AbstractValidator<DeleteEmployeeModel>
    {
        public DeleteEmployeeModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
        }
    }
}
