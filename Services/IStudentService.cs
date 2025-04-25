using BasicApplication.DTOs;
using BasicApplication.Models;

namespace BasicApplication.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> CreateStudentAsync(CreateStudentDTO createStudentDTO);
        Task<bool> AssignCoursesToStudentAsync(int studentId, AssignCoursesDTO assignCourseDTO);
    }
}
