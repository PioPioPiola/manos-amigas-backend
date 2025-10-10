namespace Application.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstNames { get; set; } = null!;

        public string LastNames { get; set; } = null!;

        public char IdentificationType { get; set; }

        public string IdentificationNumber { get; set; } = null!;

        public long PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; } 

        public char? Role { get; set; } 
    }
}
