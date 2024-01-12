using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Put;
using Project.HumanResources.Integration.Users.Command;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users;

public class UserController : HumanResourcesController
{
    private readonly IValidator<ChangePasswordRequestModel> validatorChangePasswordRequestModel;

    public UserController(
        ISender mediator,
        IValidator<ChangePasswordRequestModel> validatorChangePasswordRequestModel
    ) : base(mediator)
    {
        this.validatorChangePasswordRequestModel = validatorChangePasswordRequestModel;
    }

    [HttpPut("change-password")]
    public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> ChangePassword(
        ChangePasswordRequestModel request
    )
    {
        var validator = await this.validatorChangePasswordRequestModel.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var command = new ChangePasswordCommand($"{request.OldPassword}", $"{request.NewPassword}");

        var result = await this.Mediator.Send(command);

        var response = new ResponseBaseModel<CommandHumanResourcesBase>
        {
            Data = result.Adapt<CommandHumanResourcesBase>()
        };

        return response;
    }
}
