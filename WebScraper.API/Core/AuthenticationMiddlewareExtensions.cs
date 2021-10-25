using Microsoft.AspNetCore.Builder;

namespace WebScraper.API.Core
{
    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder UseAuthenticationExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
