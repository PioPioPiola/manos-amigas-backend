using Application.DTOs;
using Application.Services;
using Infrastructure.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IRevocationStore _revocationStore;

        public AuthController(AuthService authService, IRevocationStore revocationStore)
        {
            _authService = authService;
            _revocationStore=revocationStore;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto);

                var data = new
                {
                    token = result.Token,
                    id = result.Person.person_id,
                    email = result.Person.person_id,
                    first_names = result.Person.first_names,
                    last_names = result.Person.last_names,
                    rol = result.Person.role
                };
                return Ok(data);
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);

                var data = new
                {
                    token = result.Token,
                    id = result.Person.person_id,
                    email = result.Person.person_id,
                    first_names = result.Person.first_names,
                    last_names = result.Person.last_names,
                    rol = result.Person.role
                };
                return Ok(data);
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var expClaim = User.FindFirstValue(JwtRegisteredClaimNames.Exp);
            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(expClaim))
            {
                await _revocationStore.RevokeAsync(jti ?? Guid.NewGuid().ToString(), TimeSpan.FromMinutes(15));
                return Ok();
            }

            if (!long.TryParse(expClaim, out var expUnix))
            {
                await _revocationStore.RevokeAsync(jti, TimeSpan.FromMinutes(15));
                return Ok();
            }

            var expiresAt = DateTimeOffset.FromUnixTimeSeconds(expUnix);
            var ttl = expiresAt - DateTimeOffset.UtcNow;
            if (ttl < TimeSpan.Zero) ttl = TimeSpan.Zero;

            if (ttl > TimeSpan.Zero)
                await _revocationStore.RevokeAsync(jti, ttl);

            return Ok();
        }
    }
}
