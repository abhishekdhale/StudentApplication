using System.ComponentModel.DataAnnotations;

namespace BasicApplication.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

       public String Courses { get; set; }


    }

    public class CreateStudentDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }

    public class AssignCoursesDTO
    {
        [Required]
        public List<int> CourseIds { get; set; }
    }
}
