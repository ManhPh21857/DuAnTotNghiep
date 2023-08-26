using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Get;
using Project.HumanResources.Integration.Users.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users;

public class UserController : HumanResourcesController {
    public UserController(ISender mediator) : base(mediator) {
    }

    [HttpGet("")]
    public async Task<ActionResult<ResponseBaseModel<UsersModel>>> GetUsers() {
        //create query
        var query = new UsersQuery();

        //result
        var result = await Mediator.Send(query);

        //response
        var response = new ResponseBaseModel<UsersModel> {
            Data = result.Adapt<UsersModel>()
        };

        return response;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseBaseModel<UserModel>>> GetUser(int id) {
        //create query
        var query = new UserQuery(id);

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