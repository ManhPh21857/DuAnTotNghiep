using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Infrastructure.WebAPI.CoreController;

namespace Project.Product.Infrastructure.WebAPI.Controllers
{
    [Route("api/v{v:apiVersion}/common/[controller]")]
    public abstract class CommonController : CoreController
    {
        protected CommonController(ISender mediator) : base(mediator)
        {
        }

        protected CommonController() : base(null)
        {
        }
    }
}
