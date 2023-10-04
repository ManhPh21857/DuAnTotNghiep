using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Infrastructure.WebAPI.CoreController;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers;

[Route("api/v{v:apiVersion}/human-resources/[controller]")]
public abstract class HumanResourcesController : CoreController
{
    protected HumanResourcesController(ISender mediator) : base(mediator)
    {
    }

    protected HumanResourcesController() : base(null)
    {
    }
}