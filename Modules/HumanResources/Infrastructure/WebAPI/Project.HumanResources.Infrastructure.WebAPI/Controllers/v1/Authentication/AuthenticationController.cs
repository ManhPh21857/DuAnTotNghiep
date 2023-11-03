using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project.Core.Domain.Enums;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Login;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Register;
using Project.HumanResources.Integration.Authentication.Login;
using Project.HumanResources.Integration.Authentication.Register;
using Project.HumanResources.Integration.Authentication.VerifyEmail;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication;

[AllowAnonymous]
[Route("api/v{v:apiVersion}/[controller]")]
public class AuthenticationController : HumanResourcesController
{
    private readonly IValidator<LoginRequestModel> loginValidator;
    private readonly IValidator<RegisterRequestModel> registerValidator;
    private readonly IValidator<EmailAuthenticationRequestModel> emailValidator;
    private readonly IMemoryCache memoryCache;

    public AuthenticationController(
        ISender mediator,
        IValidator<LoginRequestModel> loginValidator,
        IValidator<RegisterRequestModel> registerValidator,
        IValidator<EmailAuthenticationRequestModel> emailValidator,
        IMemoryCache memoryCache
    ) : base(mediator)
    {
        this.loginValidator = loginValidator;
        this.registerValidator = registerValidator;
        this.emailValidator = emailValidator;
        this.memoryCache = memoryCache;
    }

    [HttpPost("employee/login")]
    public async Task<ActionResult<ResponseBaseModel<LoginResponseModel>>> EmployeeLogin(
        [FromBody] LoginRequestModel request)
    {
        var validator = await loginValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState);
            return this.BadRequest(ModelState);
        }

        var loginRequest = request.Adapt<LoginRequest>();

        var result = await Mediator.Send(loginRequest);

        var response = new ResponseBaseModel<LoginResponseModel>
        {
            Data = result.Adapt<LoginResponseModel>()
        };

        return response;
    }


    [HttpPost("login")]
    public async Task<ActionResult<ResponseBaseModel<LoginResponseModel>>> Login(
        [FromBody] LoginRequestModel request)
    {
        var validator = await loginValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState);
            return this.BadRequest(ModelState);
        }

        var loginRequest = request.Adapt<LoginRequest>();

        var result = await Mediator.Send(loginRequest);

        var response = new ResponseBaseModel<LoginResponseModel>
        {
            Data = result.Adapt<LoginResponseModel>()
        };

        return response;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResponseBaseModel<RegisterResponseModel>>> Register(
        [FromBody] RegisterRequestModel request)
    {
        var validator = await registerValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState);
            return this.BadRequest(ModelState);
        }

        var registerRequest = request.Adapt<RegisterRequest>();
        registerRequest.Roles = new List<Role> { Role.Customer };

        var result = await Mediator.Send(registerRequest);

        var response = new ResponseBaseModel<RegisterResponseModel>
        {
            Data = result.Adapt<RegisterResponseModel>()
        };

        return response;
    }

    [HttpPost("send-email")]
    public async Task<ActionResult<ResponseBaseModel<EmailAuthenticationResponseModel>>> SendMail(
        [FromBody] EmailAuthenticationRequestModel request)
    {
        var validator = await emailValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState);
            return this.BadRequest(ModelState);
        }

        var sendMailRequest = request.Adapt<VerifyEmailRequest>();

        var result = await Mediator.Send(sendMailRequest);

        var response = new ResponseBaseModel<EmailAuthenticationResponseModel>
        {
            Data = result.Adapt<EmailAuthenticationResponseModel>()
        };

        return response;
    }

    [HttpGet("verify-email/{code}")]
    public async Task<ActionResult<ResponseBaseModel<VerifyEmailResponseModel>>> VerifyMail(string code)
    {
        memoryCache.TryGetValue(nameof(VerifyEmailResponse.VerificationCodes), out string value);

        var response = new ResponseBaseModel<VerifyEmailResponseModel>
        {
            Data = new VerifyEmailResponseModel
            {
                IsSuccess = code == value
            }
        };

        return response;
    }
}