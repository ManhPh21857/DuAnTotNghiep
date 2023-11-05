using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Put;
using Project.Product.Integration.Classifications.Command;
using Project.Product.Integration.Classifications.Query;



namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications
{
    public class ClassificationController : CommonController
    {
        private readonly IValidator<AddClassificationsModel> addClassificationsValidator;
        private readonly IValidator<UpdateClassificationsModel> updateClassificationsValidator;
        private readonly IValidator<DeleteClassificationsModel> deleteClassificationsValidator;
        public ClassificationController(ISender meadiator,
            IValidator<AddClassificationsModel> addClassificationsValidator,
            IValidator<UpdateClassificationsModel> updateClassificationsValidator,
            IValidator<DeleteClassificationsModel> deleteClassificationsValidator
            ) : base(meadiator)
        {
            this.addClassificationsValidator = addClassificationsValidator;
            this.updateClassificationsValidator = updateClassificationsValidator;
            this.deleteClassificationsValidator = deleteClassificationsValidator;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseBaseModel<GetClassificationsReponseModel>> GetClassifications()
        {
            var result = await Mediator.Send(new GetClassificationQuery());

            return new ResponseBaseModel<GetClassificationsReponseModel>
            {
                Data = result.Adapt<GetClassificationsReponseModel>()
            };
        }



        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult<ResponseBaseModel<AddClassificationsReponseModel>>> AddClassifications(
       [FromBody] AddClassificationsModel request)
        {
            var validator = await addClassificationsValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }
            var registerRequest = request.Adapt<ReactiveClassificationCommand>();
            var result = await Mediator.Send(registerRequest);
            var response = new ResponseBaseModel<AddClassificationsReponseModel>
            {
                Data = result.Adapt<AddClassificationsReponseModel>()
            };
            return response;
        }

        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<ResponseBaseModel<UpdateClassificationsReponseModel>>> UpdateClassifications(
       [FromBody] UpdateClassificationsModel request)
        {           
            var validator= await updateClassificationsValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }
            var registerRequest = request.Adapt<UpdateClassificationCommand>();
            var result = await Mediator.Send(registerRequest);
            var response = new ResponseBaseModel<UpdateClassificationsReponseModel>
            {
                Data = result.Adapt<UpdateClassificationsReponseModel>()
            };
            return response;
        }



        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseBaseModel<DeleteClassificationsReponseModel>>> DeleteClassifications(int id)
        {

            var registerRequest = new DeleteClassificationCommand(id, null);

            var result = await Mediator.Send(registerRequest);

            var response = new ResponseBaseModel<DeleteClassificationsReponseModel>
            {
                Data = result.Adapt<DeleteClassificationsReponseModel>()
            };
            return response;
        }
    }
}
