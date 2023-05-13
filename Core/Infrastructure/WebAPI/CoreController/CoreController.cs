using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Core.Infrastructure.WebAPI.CoreController; 

[ApiController]
[ApiVersion("1.0")]
public abstract class CoreController : ControllerBase {
    private ISender? mediator;

    protected ISender Mediator => mediator ??= HttpContext.RequestServices.GetService<ISender>()!;

    protected CoreController(ISender mediator) {
        this.mediator = mediator;
    }
}