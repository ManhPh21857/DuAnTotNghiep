using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication
{
    public class TokenController : HumanResourcesController
    {
        public TokenController(ISender mediator) : base(mediator)
        {
        }

        [HttpGet("expire")]
        public bool CheckTokenExpire()
        {
            return true;
        }
    }
}
