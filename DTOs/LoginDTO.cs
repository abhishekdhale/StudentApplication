using System.ComponentModel.DataAnnotations;

namespace BasicApplication.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Username {  get; set; }

        [Required]
        public string Password {  get; set; }
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
