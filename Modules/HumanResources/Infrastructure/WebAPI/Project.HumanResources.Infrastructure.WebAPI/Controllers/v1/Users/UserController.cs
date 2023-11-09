using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Get;
using Project.HumanResources.Integration.Users.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users;

public class UserController : HumanResourcesController
{
    public UserController(ISender mediator) : base(mediator)
    {
    }

    [Authorize(Roles = nameof(Role.Customer))]
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

    [Authorize(Roles = nameof(Role.Admin))]
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
}