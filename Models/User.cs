using System.ComponentModel.DataAnnotations;

namespace BasicApplication.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public String Username { get; set; }

        [Required]
        public String PasswordHash {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
