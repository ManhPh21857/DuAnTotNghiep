using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.VerifyEmail;
using Project.HumanResources.Integration.Services;
using System.Net;

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

                break;
            case SendMailMode.Forgot:
                if (user is null)
                {
                    throw new DomainException("", "email not exist!");
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
}
