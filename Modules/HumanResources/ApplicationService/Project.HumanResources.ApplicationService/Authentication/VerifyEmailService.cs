using EmailValidator.NET;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.VerifyEmail;
using Project.HumanResources.Integration.Services;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Project.HumanResources.ApplicationService.Authentication;

public class VerifyEmailService : CommandHandler<VerifyEmailRequest, VerifyEmailResponse>
{
    private readonly ISender mediator;
    private readonly IMemoryCache memoryCache;
    private readonly IUserRepository userRepository;

    public VerifyEmailService(ISender mediator, IMemoryCache memoryCache, IUserRepository userRepository)
    {
        this.mediator = mediator;
        this.memoryCache = memoryCache;
        this.userRepository = userRepository;
    }

    public async override Task<VerifyEmailResponse> Handle(
        VerifyEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = (await this.userRepository.GetUserRegister(request.Email, null)).SingleOrDefault();

        switch (request.Mode)
        {
            case SendMailMode.Create:
                if (user is not null)
                {
                    throw new DomainException("", "email already exist!");
                }

                if (!this.VerifyEmail(request.Email))
                {
                    throw new DomainException("", "Email không tồn tại");
                }

                break;
            case SendMailMode.Forgot:
                if (user is null)
                {
                    throw new DomainException("", "email not exist!");
                }

                var roles = await this.userRepository.GetUserRoles(user.Id);
                if (roles.All(x => x.Id != Role.ShopLogin.GetHashCode()))
                {
                    throw new DomainException("", "tài khoản không có quyền đăng nhập");
                }

                break;
            default:
                throw new DomainException(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    nameof(HttpStatusCode.BadRequest));
        }

        var generator = new Random();
        int r = generator.Next(1, 999999);
        string code = r.ToString().PadLeft(6, '0');

        var sendTo = new List<string>
        {
            request.Email
        };

        var sendMailRequest = new SendMailRequest(
            sendTo,
            "Accuracy Email",
            $"Your verification codes is : {code}"
        );

        var cacheExpiryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromMinutes(5),
            Size = 1024,
        };

        var result = await this.mediator.Send(sendMailRequest, CancellationToken.None);
        if (!result.IsSuccess)
        {
            throw new DomainException(
                HttpStatusCode.BadRequest.GetHashCode().ToString(),
                nameof(HttpStatusCode.BadRequest)
            );
        }

        this.memoryCache.Set(request.Email, code, cacheExpiryOptions);

        return new VerifyEmailResponse(code);
    }

    public bool VerifyEmail(string emailVerify)
    {
        var emailValidator = new EmailValidator.NET.EmailValidator();

        if (!emailValidator.Validate(emailVerify, out var result))
        {
            return false;
        }

        if (result == EmailValidationResult.OK)
        {
            return true;
        }

        return false;
    }
}
