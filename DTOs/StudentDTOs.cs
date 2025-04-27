using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs
{
    public class CreateStudentRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }

    public class AssignCoursesRequest
    {
        [Required]
        public List<int> CourseIds { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Courses { get; set; }
    }
} 