using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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

        [Authorize] 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var expClaim = User.FindFirstValue(JwtRegisteredClaimNames.Exp);

            await _authService.RevokeTokenAsync(jti, expClaim);

            return Ok();
        }
    }
}
