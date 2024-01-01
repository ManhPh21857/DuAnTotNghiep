using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get;
using Project.HumanResources.Integration.Roles;
using SixLabors.ImageSharp;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles
{
    public class RoleController : HumanResourcesController
    {
        public RoleController(ISender mediator) : base(mediator)
        {
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet]
        public async Task<ResponseBaseModel<GetRolesResponseModel>> GetRoles()
        {
            var query = new RoleQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetRolesResponseModel>
            {
                Data = result.Adapt<GetRolesResponseModel>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("group")]
        public async Task<ResponseBaseModel<GetGroupsResponseModel>> GetGroups()
        {
            var query = new GroupQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetGroupsResponseModel>
            {
                Data = result.Adapt<GetGroupsResponseModel>()
            };
            
            return response;
        }

        [AllowAnonymous]
        [HttpPost("image")]
        public async Task<ResponseBaseModel<int>> UploadImage(UploadImageRequest request)
        {
            var imageBytes = Convert.FromBase64String(request.Image);
            using var image = Image.Load(imageBytes);
            await image.SaveAsync(@"D:\Final\Img\foo.jpg");



            return new ResponseBaseModel<int>()
            {
                Data = 1
            };
        }
    }

    public class UploadImageRequest
    {
        public string Image { get; set; }
    }
}
