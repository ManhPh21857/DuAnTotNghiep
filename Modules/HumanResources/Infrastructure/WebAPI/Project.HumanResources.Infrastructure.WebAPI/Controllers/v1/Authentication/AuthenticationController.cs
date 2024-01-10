using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project.Core.Domain;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Forgot;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Login;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Register;
using Project.HumanResources.Integration.Authentication.Forgot;
using Project.HumanResources.Integration.Authentication.Login;
using Project.HumanResources.Integration.Authentication.Register;
using Project.HumanResources.Integration.Authentication.VerifyEmail;
using Project.HumanResources.Integration.Employees.Login;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication;

[AllowAnonymous]
[Route("api/v{v:apiVersion}/[controller]")]
public class AuthenticationController : HumanResourcesController
{
    private readonly IValidator<LoginRequestModel> loginValidator;
    private readonly IValidator<RegisterRequestModel> registerValidator;
    private readonly IValidator<EmailAuthenticationRequestModel> emailValidator;
    private readonly IValidator<ForgotRequestModel> forgotValidator;
    private readonly IMemoryCache memoryCache;

    public AuthenticationController(
        ISender mediator,
        IValidator<LoginRequestModel> loginValidator,
        IValidator<RegisterRequestModel> registerValidator,
        IValidator<EmailAuthenticationRequestModel> emailValidator,
        IValidator<ForgotRequestModel> forgotValidator,
        IMemoryCache memoryCache
    ) : base(mediator)
    {
        this.loginValidator = loginValidator;
        this.registerValidator = registerValidator;
        this.emailValidator = emailValidator;
        this.forgotValidator = forgotValidator;
        this.memoryCache = memoryCache;
    }

    [HttpPost("employee/login")]
    public async Task<ActionResult<ResponseBaseModel<LoginResponseModel>>> EmployeeLogin(
        [FromBody] LoginRequestModel request
    )
    {
        var validator = await this.loginValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var loginRequest = request.Adapt<EmployeeLoginRequest>();

        var result = await this.Mediator.Send(loginRequest);

        var response = new ResponseBaseModel<LoginResponseModel>
        {
            Data = result.Adapt<LoginResponseModel>()
        };

        return response;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseBaseModel<LoginResponseModel>>> Login(
        [FromBody] LoginRequestModel request
    )
    {
        var validator = await this.loginValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var loginRequest = request.Adapt<LoginRequest>();

        var result = await this.Mediator.Send(loginRequest);

        var response = new ResponseBaseModel<LoginResponseModel>
        {
            Data = result.Adapt<LoginResponseModel>()
        };

        return response;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResponseBaseModel<RegisterResponseModel>>> Register(
        [FromBody] RegisterRequestModel request
    )
    {
        var validator = await this.registerValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var registerRequest = request.Adapt<RegisterRequest>();

        var result = await this.Mediator.Send(registerRequest);

        var response = new ResponseBaseModel<RegisterResponseModel>
        {
            Data = result.Adapt<RegisterResponseModel>()
        };

        return response;
    }

    [HttpPost("send-email")]
    public async Task<ActionResult<ResponseBaseModel<EmailAuthenticationResponseModel>>> SendMail(
        [FromBody] EmailAuthenticationRequestModel request
    )
    {
        var validator = await this.emailValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var sendMailRequest = request.Adapt<VerifyEmailRequest>();

        var result = await this.Mediator.Send(sendMailRequest);

        var response = new ResponseBaseModel<EmailAuthenticationResponseModel>
        {
            Data = result.Adapt<EmailAuthenticationResponseModel>()
        };

        return response;
    }

    [HttpPost("verify-email")]
    public Task<ActionResult<ResponseBaseModel<VerifyEmailResponseModel>>> VerifyMail(
        [FromBody] VerifyEmailRequestModel request
    )
    {
        this.memoryCache.TryGetValue(request.Email, out string value);

        var response = new ResponseBaseModel<VerifyEmailResponseModel>
        {
            Data = new VerifyEmailResponseModel
            {
                IsSuccess = request.Code == value
            }
        };

        return Task.FromResult<ActionResult<ResponseBaseModel<VerifyEmailResponseModel>>>(response);
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<ResponseBaseModel<ForgotResponseModel>>> ForgotPassword(
        [FromBody] ForgotRequestModel request
    )
    {
        var validator = await this.forgotValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var sendMailRequest = request.Adapt<ForgotRequest>();

        var result = await this.Mediator.Send(sendMailRequest);

        var response = new ResponseBaseModel<ForgotResponseModel>
        {
            Data = result.Adapt<ForgotResponseModel>()
        };

        return response;
    }
}
