using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Delete;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Post;
using Project.HumanResources.Integration.Roles.Command;
using Project.HumanResources.Integration.Roles.Query;
using System.Text.RegularExpressions;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles
{
    public class RoleController : HumanResourcesController
    {
        private readonly IValidator<UpdateGroupRoleRequestModel> validatorUpdateGroupRoleRequestModel;

        public RoleController(
            ISender mediator,
            IValidator<UpdateGroupRoleRequestModel> validatorUpdateGroupRoleRequestModel
        ) : base(mediator)
        {
            this.validatorUpdateGroupRoleRequestModel = validatorUpdateGroupRoleRequestModel;
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

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("group")]
        public async Task<ActionResult<ResponseBaseModel<CommandHumanResourcesBase>>> UpdateGroupRole(
            UpdateGroupRoleRequestModel request
        )
        {
            var validator = await this.validatorUpdateGroupRoleRequestModel.ValidateAsync(request);
            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    throw new DomainException(error.PropertyName, error.ErrorMessage);
                }
            }

            var command = request.Adapt<UpdateGroupRoleQuery>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("group/{pageNo}")]
        public async Task<ResponseBaseModel<GetGroupListResponseModel>> GetGroups(int pageNo)
        {
            var query = new GetGroupQuery(pageNo);

            var result = await this.Mediator.Send(query);
            
            var response = new ResponseBaseModel<GetGroupListResponseModel>
            {
                Data = result.Adapt<GetGroupListResponseModel>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("group-role/{groupId}")]
        public async Task<ResponseBaseModel<GetGroupRoleResponseModel>> GetGroupRoles(int groupId)
        {
            var query = new GetGroupRoleQuery(groupId);

            var result = await this.Mediator.Send(query);
            
            var response = new ResponseBaseModel<GetGroupRoleResponseModel>
            {
                Data = result.Adapt<GetGroupRoleResponseModel>()
            };

            return response;
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPut("group/delete")]
        public async Task<ResponseBaseModel<CommandHumanResourcesBase>> DeleteRole(DeleteGroupRequestModel request)
        {
            var command = request.Adapt<DeleteGroupCommand>();

            var result = await this.Mediator.Send(command);
            
            var response = new ResponseBaseModel<CommandHumanResourcesBase>
            {
                Data = result.Adapt<CommandHumanResourcesBase>()
            };

            return response;
        }
    }
}
