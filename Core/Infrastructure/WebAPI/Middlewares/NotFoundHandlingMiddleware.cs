using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Project.Core.Infrastructure.WebAPI.Middlewares
{
    public class NotFoundHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public NotFoundHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleWare> logger)
        {
            if (context.Request.Method == "OPTIONS" || context.Request.Path.Value.ToLower().Contains("signalr"))
            {
                await next(context);
                return;
            }

            var enableSimulate = configuration.GetValue<bool>("ENABLE_SIMULATE_404");
            if (enableSimulate)
            {
                await ResponseWrite404(context);
            }
            else
            {
                await next(context);
                if (context.Request.HttpContext.Response.StatusCode == 404)
                {
                    await ResponseWrite404(context);
                }
            }
        }

        private async Task ResponseWrite404(HttpContext context)
        {
            string? result = JsonSerializer.Serialize(
              new
              {
                  errorCode = "404",
                  error = $" No HTTP resource was found that matches the request URI '{GetFullUrl(context.Request)}",
                  errorMessageKey = "",
                  errorMessageParam = new object[] { }
              }
            );

            context.Response.Headers.TryGetValue("access-control-allow-credentials", out var accessControlAllowCredentials);
            context.Response.Headers.TryGetValue("access-control-allow-origin", out var accessControlAllowOrigin);
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 404;
            context.Response.Headers.Add("access-control-allow-credentials", accessControlAllowCredentials);
            context.Response.Headers.Add("access-control-allow-origin", accessControlAllowOrigin);
            await context.Response.WriteAsync(result);
        }

        private string GetFullUrl(HttpRequest request)
        {
            var queryStrings = string.Join("&", request.Query.Select(x => $"{x.Key}={x.Value.ToString()}"));
            var httpPrefix = request.IsHttps ? "https" : "http";
            if (!string.IsNullOrWhiteSpace(queryStrings))
            {
                return $"{httpPrefix}://{request.Host}{request.Path}?{queryStrings}";
            }
            return $"{httpPrefix}://{request.Host}{request.Path}";
        }
    }
}
