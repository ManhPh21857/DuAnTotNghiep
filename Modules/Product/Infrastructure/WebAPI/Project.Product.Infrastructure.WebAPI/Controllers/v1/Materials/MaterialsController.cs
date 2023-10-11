using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Get;
using Project.Product.Integration.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials
{
    public class MaterialsController : ProductController
    {
        public MaterialsController(ISender meadiator) : base(meadiator)
        {

        }
        [AllowAnonymous]
        [HttpGet("materials")]
        public async Task<ResponseBaseModel<GetMaterialsReponseModel>> GetColors()
        {
            var result = await Mediator.Send(new GetMaterialsQuery());

            return new ResponseBaseModel<GetMaterialsReponseModel>
            {
                Data = result.Adapt<GetMaterialsReponseModel>()
            };
        }
    }
}
