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
    [Route("api/enrollment")]
    public class ProviderCategoryEnrollmentController : ControllerBase
    {
        private readonly ProviderCategoryEnrollmentService _enrollmentService;

        public ProviderCategoryEnrollmentController(ProviderCategoryEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollments();
                return Ok(enrollments);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al obtener inscripciones: {ex.Message}");
            }
        }

        [HttpGet("{personId}/status/{categoryId}")]
        public async Task<IActionResult> CheckEnrollmentStatus([FromRoute] string personId, [FromRoute] int categoryId)
        {
            try
            {
                var isEnrolled = await _enrollmentService.HasEnrollmentForCategoryAsync(personId, categoryId);
                return Ok(isEnrolled); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al verificar inscripción: {ex.Message}");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateEnrollment([FromBody] ProviderCategoryEnrollmentDto enrollmentDto)
        {
            try
            {
                await _enrollmentService.CreateAsync(enrollmentDto);

                return StatusCode(201, new { message = "Inscripción creada correctamente. Documentos en proceso de revisión." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la inscripción: {ex.Message}");
            }
        }
    }
}
