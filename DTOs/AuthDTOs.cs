using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
} 