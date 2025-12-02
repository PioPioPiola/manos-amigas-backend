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
    public class ServiceController : ControllerBase
    {

        private readonly ServiceService _serviceService;

        public ServiceController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpPost("")]
        public async Task<IActionResult> CreateServiceRequest([FromBody] ServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var createdService = await _serviceService.CreateServiceRequestAsync(serviceDto);

                return StatusCode(201, createdService);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno: {ex.Message}" });
            }
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetServiceById([FromRoute] long serviceId)
        {
            try
            {
                var service = await _serviceService.GetServiceByIdAsync(serviceId);
                return Ok(service); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener el servicio: {ex.Message}" }); 
            }
        }
    }
}
