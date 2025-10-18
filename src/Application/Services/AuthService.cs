using Application.DTOs;
using Application.Interfaces;
using ManoaAmigas.Domain.Utilities;
using ManoaAmigas.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Infrastructure.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthResult
    {
        public string Token { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public Person PersonDocument { get; set; } = null!;
    }

    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRevocationStore _revocationStore;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IRevocationStore revocationStore)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _revocationStore = revocationStore;
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new InvalidOperationException("Ya existe el usuario con el email registrado");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var person = new Person
            {
                person_id = AuthHelpers.GenerateNumericId(),
                email = dto.Email,
                password_hash = hash,
                name = dto.Name,
                first_names = dto.Name,
                last_names = dto.Name,
                identification_number = dto.IdentificationNumber,
                identification_type = dto.IdentificationType,
                phone_number = dto.PhoneNumber,
                date_of_birth = DateTime.UtcNow,
                role = dto.Role,
                registration_date = DateTime.UtcNow,
                account_status = 'U', //Every new user is unverified
                meets_requirements = false,
                last_updated = DateTime.UtcNow,
                gender = dto.Gender,
                address = dto.Address,
                department = dto.Department,
                city = dto.City,
                postal_code = dto.PostalCode,
                security_question = dto.SecurityQuestion,
                security_answer = dto.SecurityAnswer,
                accept_terms = dto.AcceptTerms,
                accept_data_policy = dto.AcceptDataPolicy,
                wants_notifications = dto.WantsNotifications
            };

            var personDocument = new PersonDocument
            {
                person_id = person.person_id,
                doctype_id = dto.DocTypeId,
                issue_date = DateTime.UtcNow,
                issue_place = dto.IssuePlace
            };

            await _userRepository.CreateAsync(person);
            await _userRepository.CreateAsync(personDocument);

            var token = GenerateToken(person);
            return new AuthResult { Token = token, Person = person };
        }

        public async Task<AuthResult> LoginAsync(LoginDto dto)
        {
            var person = await _userRepository.GetByEmailAsync(dto.Email);

            if (person == null) throw new InvalidOperationException("Usuario inválido.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, person.password_hash)) throw new InvalidOperationException("Contraseña incorrecta");

            var token = GenerateToken(person);
            return new AuthResult { Token = token, Person = person };
        }

        public async Task RevokeTokenAsync(string? jti, string? expClaim)
        {
            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(expClaim))
            {
                await _revocationStore.RevokeAsync(jti ?? Guid.NewGuid().ToString(), TimeSpan.FromMinutes(15));
                return;
            }

            if (!long.TryParse(expClaim, out var expUnix))
            {
                await _revocationStore.RevokeAsync(jti, TimeSpan.FromMinutes(15));
                return;
            }

            var expiresAt = DateTimeOffset.FromUnixTimeSeconds(expUnix);
            var ttl = expiresAt - DateTimeOffset.UtcNow;

            if (ttl < TimeSpan.Zero) ttl = TimeSpan.Zero;

            if (ttl > TimeSpan.Zero)
                await _revocationStore.RevokeAsync(jti, ttl);
        }

        private string GenerateToken(Person person)
        {
            //TO DO: Don't work with claims, create own session
            var secret = _configuration["Jwt:Key"] ?? "PioT0W4SgK7pQpXyVbJ9mFhLcVzG3ManoseR8nUaE1iZ2oD6YxAmigasCwB5qI4tP0uHlJ2rK6sM8Pio";
            var issuer = _configuration["Jwt:Issuer"] ?? "ManosAmigas";
            var audience = _configuration["Jwt:Audience"] ?? "ManosAmigasClients";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jti = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, person.person_id),
                new Claim(JwtRegisteredClaimNames.Email, person.email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
