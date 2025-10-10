using Infrastructure.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class JwtRevocationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRevocationMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx, IRevocationStore store)
        {
            if (ctx.User?.Identity?.IsAuthenticated == true)
            {
                var jti = ctx.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                var personId = ctx.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (!string.IsNullOrEmpty(jti) && await store.IsRevokedAsync(jti))
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await ctx.Response.WriteAsync("Token revoked");
                    return;
                }
            }

            await _next(ctx);
        }
    }
}
