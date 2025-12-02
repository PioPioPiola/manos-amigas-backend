using Application.DTOs;
using Application.Services;
using Domain.Enums;
using ManoaAmigas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPersons([FromQuery] char? accountStatus)
        {
            try
            {
                var persons = await _personService.GetAllPersonAsync(accountStatus);

                return Ok(persons);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPatch("{personId}")]
        public async Task<IActionResult> UpdateVerificationStatus([FromRoute] string personId, [FromBody] UpdateAccountStatusDto dto)
        {
            try
            {
                var success = await _personService.UpdatePersonVerificationStatusAsync(personId, dto.AccountStatus);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
