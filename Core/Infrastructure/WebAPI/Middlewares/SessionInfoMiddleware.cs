using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Project.Core.Domain;
using System.Security.Claims;
using Project.Core.Domain.Login;
using Project.Core.Domain.User;

namespace Project.Core.Infrastructure.WebAPI.Middlewares
{
    public class SessionInfoMiddleware
    {
        private readonly RequestDelegate next;

        public SessionInfoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISessionInfo sessionInfo)
        {
            var principal = context.User;
            if (sessionInfo is SessionInfo session && principal != null && principal.Identity != null && principal.Claims.Any())
            {
                var userId = new UserId(int.Parse(principal.FindFirstValue(nameof(UserId).ToLower())));

                var sessionId = new SessionId(Guid.Parse(principal.FindFirstValue(JwtRegisteredClaimNames.Jti)));

                session.Initialize(
                    userId,
                    sessionId
                );
            }
            await next(context);
        }
    }
}
