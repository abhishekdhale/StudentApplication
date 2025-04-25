using System.ComponentModel.DataAnnotations;

namespace BasicApplication.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email;

        [Required]
        [StringLength(20)]
        public string Phone {  get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
