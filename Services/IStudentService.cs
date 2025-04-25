using StudentManagement.DTOs;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request);
        Task AssignCoursesAsync(int studentId, AssignCoursesRequest request);
        Task<List<StudentResponse>> GetAllStudentsAsync();
    }
} 