namespace Application.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public char Rol { get; set; }

        public bool? RememberMe { get; set; } = false;
    }
}
