using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Infrastructure.WebAPI.CoreController;

namespace Project.Product.Infrastructure.WebAPI.Controllers
{
    [Route("api/v{v:apiVersion}/product/[controller]")]
    public abstract class ProductController : CoreController
    {
        protected ProductController(ISender mediator) : base(mediator)
        {
        }

        protected ProductController() : base(null)
        {
        }
    }
}
