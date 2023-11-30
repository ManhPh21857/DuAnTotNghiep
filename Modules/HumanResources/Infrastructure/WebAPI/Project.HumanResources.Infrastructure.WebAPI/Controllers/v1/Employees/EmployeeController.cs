using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Get;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Post;
using Project.HumanResources.Integration.Employees.Command;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees
{
    public class EmployeeController : HumanResourcesController
    {
        private readonly IValidator<UpdateEmployeeRequestModel> validatorUpdateEmployeeRequestModel;

        public EmployeeController(
            ISender mediator,
            IValidator<UpdateEmployeeRequestModel> validatorUpdateEmployeeRequestModel
        ) : base(mediator)
        {
            this.validatorUpdateEmployeeRequestModel = validatorUpdateEmployeeRequestModel;
        }

        [Authorize(Roles = nameof(Role.UserView))]
        [HttpGet("{pageNumber}")]
        public async Task<ActionResult<ResponseBaseModel<GetEmployeeResponseModel>>> GetEmployee(int pageNumber)
        {
            var query = new EmployeeQuery(pageNumber);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetEmployeeResponseModel>
            {
                Data = result.Adapt<GetEmployeeResponseModel>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.UserEdit))]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<UpdateEmployeeResponseModel>>> UpdateEmployee(
            UpdateEmployeeRequestModel request
        )
        {
            var validator = await this.validatorUpdateEmployeeRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateEmployeeCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<UpdateEmployeeResponseModel>
            {
                Data = result.Adapt<UpdateEmployeeResponseModel>()
            };

            return response;
        }
    }
}
