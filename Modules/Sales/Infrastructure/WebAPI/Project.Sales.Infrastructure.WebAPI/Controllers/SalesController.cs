using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Infrastructure.WebAPI.CoreController;

namespace Project.Sales.Infrastructure.WebAPI.Controllers
{
    [Route("api/v{v:apiVersion}/sales/[controller]")]
    public abstract class SalesController : CoreController
    {
        protected SalesController(ISender mediator) : base(mediator)
        {
        }

        protected SalesController() : base(null)
        {
        }
    }
}
