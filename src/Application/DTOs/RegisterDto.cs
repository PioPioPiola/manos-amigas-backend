namespace Application.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        //public string FirstNames { get; set; } = null!;

        //public string LastNames { get; set; } = null!;

        public char IdentificationType { get; set; }

        public string IdentificationNumber { get; set; } = null!;

        public long PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; } 

        public char? Role { get; set; }

        public char? Gender { get; set; }

        public int DocTypeId { get; set; }

        public string? DocumentNumber { get; set; } = null;

        public DateTime IssueDate { get; set; }

        public string IssuePlace { get; set; } = null!;

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Department { get; set; }

        public string? PostalCode { get; set; }

        public char? SecurityQuestion { get; set; }

        public string? SecurityAnswer { get; set; }

        public bool AcceptTerms { get; set; }

        public bool AcceptDataPolicy { get; set; }

        public bool WantsNotifications { get; set; }
    }
}
