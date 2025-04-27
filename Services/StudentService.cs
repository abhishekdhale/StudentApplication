using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.DTOs;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request)
        {
            var student = new Student
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Courses = string.Empty
            };
        }

        public async Task AssignCoursesAsync(int studentId, AssignCoursesRequest request)
        {
            var student = await _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {studentId} not found");
            }

            var existingCourseIds = student.StudentCourses.Select(sc => sc.CourseId).ToList();
            var newCourseIds = request.CourseIds.Except(existingCourseIds).ToList();

            foreach (var courseId in newCourseIds)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {courseId} not found");
                }

                student.StudentCourses.Add(new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentResponse>> GetAllStudentsAsync()
        {
            var students = await _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .ToListAsync();

            return students.Select(s => new StudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Courses = string.Join(", ", s.StudentCourses.Select(sc => sc.Course.Name))
            }).ToList();
        }
    }
} 