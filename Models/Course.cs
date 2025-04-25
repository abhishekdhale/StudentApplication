using System.ComponentModel.DataAnnotations;

namespace BasicApplication.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
