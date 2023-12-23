using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Get;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Post;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Put;
using Project.HumanResources.Integration.Customers.Command;
using Project.HumanResources.Integration.Customers.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers
{
    public class CustomerController : HumanResourcesController
    {
        private readonly IValidator<UpdateCustomerModel> validatorUpdateCustomerModel;
        private readonly IValidator<UpdateAddressRequestModel> validatorUpdateAddressRequestModel;
        private readonly IValidator<UpdateDefaultAddressRequest> validatorUpdateDefaultAddressRequest;

        public CustomerController(
            ISender mediator,
            IValidator<UpdateCustomerModel> validatorUpdateCustomerModel,
            IValidator<UpdateAddressRequestModel> validatorUpdateAddressRequestModel,
            IValidator<UpdateDefaultAddressRequest> validatorUpdateDefaultAddressRequest
        ) : base(mediator)
        {
            this.validatorUpdateCustomerModel = validatorUpdateCustomerModel;
            this.validatorUpdateAddressRequestModel = validatorUpdateAddressRequestModel;
            this.validatorUpdateDefaultAddressRequest = validatorUpdateDefaultAddressRequest;
        }

        [HttpGet("addresses")]
        public async Task<ActionResult<ResponseBaseModel<GetCustomerAddressResponseModel>>> GetCustomerAddress()
        {
            var query = new GetCustomerAddressQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetCustomerAddressResponseModel>
            {
                Data = result.Adapt<GetCustomerAddressResponseModel>()
            };

            return response;
        }

        [HttpPost("addresses")]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> UpdateAddress(
            UpdateAddressRequestModel request
        )
        {
            var validator = await this.validatorUpdateAddressRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateCustomerAddressCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }

        [HttpPut("addresses")]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> UpdateDefaultAddress(
            UpdateDefaultAddressRequest request
        )
        {
            var validator = await this.validatorUpdateDefaultAddressRequest.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateAddressDefaultCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetCustomerResponseModel>>> GetCustomer()
        {
            var query = new GetCustomerQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetCustomerResponseModel>
            {
                Data = result.Customer.Adapt<GetCustomerResponseModel>()
            };

            return response;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> UpdateCustomer(
            UpdateCustomerModel request
        )
        {
            var validator = await this.validatorUpdateCustomerModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateCustomerCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }
    }
}
