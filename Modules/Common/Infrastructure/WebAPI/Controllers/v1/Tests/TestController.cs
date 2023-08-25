using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.ApplicationService.Tests.Query;
using Project.Common.Infrastructure.WebAPI.Controllers.Base;
using Project.Common.Infrastructure.WebAPI.Controllers.v1.Tests.Get;

namespace Project.Common.Infrastructure.WebAPI.Controllers.v1.Tests;

public class TestController : CommonController {
    public TestController(ISender mediator) : base(mediator) {
    }

    [HttpGet("get-test/{id}")]
    public async Task<ActionResult<ResponseBaseModel<TestResponseModel>>> GetTest(int id) {
        var query = new TestQuery(id);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<TestResponseModel> {
            Data = result.Adapt<TestResponseModel>()
        };

        return response;
    }
}