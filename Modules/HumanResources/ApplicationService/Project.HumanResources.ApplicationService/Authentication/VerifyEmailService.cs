using System.Net;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Integration.Authentication.VerifyEmail;
using Project.HumanResources.Integration.Services;

namespace Project.HumanResources.ApplicationService.Authentication;

public class VerifyEmailService : CommandHandler<VerifyEmailRequest, VerifyEmailResponse>
{
    private readonly ISender mediator;
    private readonly IMemoryCache memoryCache;

    public VerifyEmailService(ISender mediator, IMemoryCache memoryCache)
    {
        this.mediator = mediator;
        this.memoryCache = memoryCache;
    }

    public async override Task<VerifyEmailResponse> Handle(
        VerifyEmailRequest request,
        CancellationToken cancellationToken
    )
    {
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

        var result = await mediator.Send(sendMailRequest, CancellationToken.None);
        if (!result.IsSuccess)
        {
            throw new DomainException(
                HttpStatusCode.BadRequest.GetHashCode().ToString(),
                nameof(HttpStatusCode.BadRequest)
            );
        }

        memoryCache.Set(nameof(VerifyEmailResponse.VerificationCodes), code, cacheExpiryOptions);

        return new VerifyEmailResponse(code);
    }
}