using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Post;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Put;
using Project.Sales.Integration.Vouchers.Command;
using Project.Sales.Integration.Vouchers.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers
{
    public class VoucherController : SalesController
    {
        private readonly IValidator<UpdateVoucherRequestModel> validatorUpdateVoucherRequestModel;
        private readonly IValidator<DeleteVoucherRequestModel> validatorDeleteVoucherRequestModel;

        public VoucherController(
            ISender mediator,
            IValidator<UpdateVoucherRequestModel> validatorUpdateVoucherRequestModel,
            IValidator<DeleteVoucherRequestModel> validatorDeleteVoucherRequestModel
        ) : base(mediator)
        {
            this.validatorUpdateVoucherRequestModel = validatorUpdateVoucherRequestModel;
            this.validatorDeleteVoucherRequestModel = validatorDeleteVoucherRequestModel;
        }

        [AllowAnonymous]
        [HttpGet("{totalPrice}")]
        public async Task<ActionResult<ResponseBaseModel<GetVoucherResponseModel>>> GetVouchers(float totalPrice)
        {
            var query = new GetVoucherQuery(totalPrice, null);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetVoucherResponseModel>
            {
                Data = result.Adapt<GetVoucherResponseModel>()
            };

            return response;
        }

        [AllowAnonymous]
        [HttpGet("info/{page}")]
        public async Task<ActionResult<ResponseBaseModel<GetAllVoucherResponseModel>>> GetVouchers(int page)
        {
            var query = new GetVoucherQuery(null, page);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetAllVoucherResponseModel>
            {
                Data = result.Adapt<GetAllVoucherResponseModel>()
            };

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> UpdateVoucher(
            UpdateVoucherRequestModel request
        )
        {
            var validator = await this.validatorUpdateVoucherRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateVoucherCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpPut("delete")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> DeleteVoucher(
            DeleteVoucherRequestModel request
        )
        {
            var validator = await this.validatorDeleteVoucherRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = new DeleteVoucherCommand(request.Id!.Value, request.DataVersion, true);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpPut("reactive")]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> ReactiveVoucher(
            DeleteVoucherRequestModel request
        )
        {
            var validator = await this.validatorDeleteVoucherRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = new DeleteVoucherCommand(request.Id!.Value, request.DataVersion, false);

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }
    }
}
