﻿using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Get;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Post;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Put;
using Project.HumanResources.Integration.Employees.Command;
using Project.HumanResources.Integration.Employees.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees
{
    public class EmployeeController : HumanResourcesController
    {
        private readonly IValidator<UpdateEmployeeRequestModel> validatorUpdateEmployeeRequestModel;
        private readonly IValidator<DeleteEmployeeRequestModel> validatorDeleteEmployeeRequestModel;

        public EmployeeController(
            ISender mediator,
            IValidator<UpdateEmployeeRequestModel> validatorUpdateEmployeeRequestModel,
            IValidator<DeleteEmployeeRequestModel> validatorDeleteEmployeeRequestModel
        ) : base(mediator)
        {
            this.validatorUpdateEmployeeRequestModel = validatorUpdateEmployeeRequestModel;
            this.validatorDeleteEmployeeRequestModel = validatorDeleteEmployeeRequestModel;
        }

        //[Authorize(Roles = nameof(Role.UserView))]
        [AllowAnonymous]
        [HttpGet("{pageNumber}")]
        public async Task<ActionResult<ResponseBaseModel<GetEmployeeResponseModel>>> GetEmployees(int pageNumber)
        {
            var query = new EmployeeQuery(pageNumber);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetEmployeeResponseModel>
            {
                Data = result.Adapt<GetEmployeeResponseModel>()
            };

            return response;
        }

        //[Authorize(Roles = nameof(Role.UserEdit))]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> UpdateEmployee(
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

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }

        //[Authorize(Roles = nameof(Role.UserView))]
        [AllowAnonymous]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponseBaseModel<EmployeeResponseModel>>> GetEmployee(int id)
        {
            var query = new GetEmployeeQuery(id);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<EmployeeResponseModel>
            {
                Data = result.Employee.Adapt<EmployeeResponseModel>()
            };

            return response;
        }

        [HttpPut("delete")]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> DeleteEmployee(
            DeleteEmployeeRequestModel request
        )
        {
            var validator = await this.validatorDeleteEmployeeRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<DeleteEmployeeCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }
    }
}
