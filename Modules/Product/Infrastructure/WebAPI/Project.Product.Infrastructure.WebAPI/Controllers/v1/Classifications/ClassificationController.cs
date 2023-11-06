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
using Project.Product.Integration.Classifications.Command;
using Project.Product.Integration.Classifications.Query;



namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications
{
    public class ClassificationController : CommonController
    {
        private readonly IValidator<UpdateClassificationsRequestModel> classificationValidator;
        public ClassificationController(ISender mediator, IValidator<UpdateClassificationsRequestModel> classificationValidator) : base(mediator)
        {
            this.classificationValidator = classificationValidator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetClassificationsReponseModel>>> GetClassifications()
        {
            var result = await this.Mediator.Send(new GetClassificationQuery());

            return new ResponseBaseModel<GetClassificationsReponseModel>
            {
                Data = result.Adapt<GetClassificationsReponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateClassification(UpdateClassificationsRequestModel request)
        {
            var validator = await this.classificationValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateClassificationCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandProductBase>> DeleteClassification(DeleteClassificationsRequestModel request)
        {

            var command = request.Adapt<DeleteClassificationCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandProductBase>> ReactiveColors(DeleteClassificationsRequestModel request)
        {

            var command = request.Adapt<ReactiveClassificationCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandProductBase>
            {
                Data = result.Adapt<CommandProductBase>()
            };
        }
    }
}
