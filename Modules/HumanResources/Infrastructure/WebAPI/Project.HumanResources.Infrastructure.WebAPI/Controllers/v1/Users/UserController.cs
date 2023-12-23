using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Get;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Put;
using Project.HumanResources.Integration.Users.Command;
using Project.HumanResources.Integration.Users.Query;

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

    [Authorize(Roles = nameof(Role.ShopLogin))]
    [HttpGet("list")]
    public async Task<ActionResult<ResponseBaseModel<UsersModel>>> GetUsers()
    {
        //create query
        var query = new UsersQuery();

        //result
        var result = await Mediator.Send(query);

        //response
        var response = new ResponseBaseModel<UsersModel>
        {
            Data = result.Adapt<UsersModel>()
        };

        return response;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseBaseModel<UserModel>>> GetUser()
    {
        //create query
        var query = new UserQuery();

        //result
        var result = await Mediator.Send(query);

        //response
        var response = new ResponseBaseModel<UserModel>
        {
            Data = result.User.Adapt<UserModel>()
        };

        return response;
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

        var command = new ChangePasswordCommand(request.OldPassword, request.NewPassword);

        var result = await this.Mediator.Send(command);

        var response = new ResponseBaseModel<CommandHumanResourcesBase>
        {
            Data = result.Adapt<CommandHumanResourcesBase>()
        };

        return response;
    }
}
