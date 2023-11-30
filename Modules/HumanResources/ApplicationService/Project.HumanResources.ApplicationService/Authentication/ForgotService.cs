using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Authentication.Forgot;
using System.Net;

namespace Project.HumanResources.ApplicationService.Authentication
{
    public class ForgotService : CommandHandler<ForgotRequest, ForgotResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IMemoryCache memoryCache;

        public ForgotService(IUserRepository userRepository, IMemoryCache memoryCache)
        {
            this.userRepository = userRepository;
            this.memoryCache = memoryCache;
        }

        public async override Task<ForgotResponse> Handle(ForgotRequest request, CancellationToken cancellationToken)
        {
            using var scope = TransactionFactory.Create();

            this.memoryCache.TryGetValue(request.Email, out string value);

            if (value is null || request.Code != value)
            {
                throw new DomainException(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    nameof(HttpStatusCode.BadRequest));
            }

            var user = await this.userRepository.GetUserRegister(request.Email, null);
            if (user.IsNullOrEmpty())
            {
                var exception = new DomainException("", "Username not exists");

                throw exception;
            }

            await this.userRepository.ForgotPassword(request.Email, request.Password);

            scope.Complete();

            return new ForgotResponse(true);
        }
    }
}
