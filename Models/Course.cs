using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
} 