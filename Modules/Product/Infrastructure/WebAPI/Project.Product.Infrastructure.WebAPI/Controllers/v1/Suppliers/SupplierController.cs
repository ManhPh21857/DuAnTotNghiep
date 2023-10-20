using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Put;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post;
using Project.Product.Integration.Suppliers.Query;
using Project.Product.Integration.Suppliers.Command;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers
{
    public class SupplierController : CommonController
    {
        private readonly IValidator<CreateSupplierModel> createSupplierValidator;
        private readonly IValidator<UpdateSupplierModel> updateSupplierValidator;
        private readonly IValidator<DeleteSupplierModel> deleteSupplierValidator;
        public SupplierController(ISender mediator,
            IValidator<CreateSupplierModel> createSupplierValidator,
            IValidator<UpdateSupplierModel> updateSupplierValidator,
            IValidator<DeleteSupplierModel> deleteSupplierValidator
                                     ) :base(mediator)
        {
            this.updateSupplierValidator = updateSupplierValidator;
            this.createSupplierValidator = createSupplierValidator;
            this.deleteSupplierValidator = deleteSupplierValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<SupplierReponseModel>> GetSupplier()
        {
            var result = await Mediator.Send(new GetSupplierQuery());
            return new ResponseBaseModel<SupplierReponseModel>
            {
                Data = result.Adapt<SupplierReponseModel>()
            };
        }
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<CreateSupplierReponseModel>>> CreateSupplier(
       [FromBody] CreateSupplierModel request)
        {
            var validator = await createSupplierValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<AddSupplierCommand>();
          

            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<CreateSupplierReponseModel>
            {
                Data = result.Adapt<CreateSupplierReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateSupplierReponseModel>>> UpdateSupplier(
       [FromBody] UpdateSupplierModel request)
        {
            var validator = await updateSupplierValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var registerRequest = request.Adapt<UpdateSupplierCommand>();


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<UpdateSupplierReponseModel>
            {
                Data = result.Adapt<UpdateSupplierReponseModel>()
            };

            return response;
        }


        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBaseModel<DeleteSupplierReponseModel>>> DeleteManufacturers(int id)
        {
          
            var registerRequest = new DeleteSupplierCommand(id);


            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteSupplierReponseModel>
            {
                Data = result.Adapt<DeleteSupplierReponseModel>()
            };

            return response;
        }
    }
}
