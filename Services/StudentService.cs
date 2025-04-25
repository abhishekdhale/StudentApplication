using BasicApplication.Data;
using BasicApplication.DTOs;
using BasicApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApplication.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    Courses = string.Join(", ", s.StudentCourses.Select(sc => sc.Course.Name))
                })
                .ToListAsync();
        }

        public async Task<StudentDTO> CreateStudentAsync(CreateStudentDTO createStudentDTO)
        {
            if (await _context.Students.AnyAsync(s => s.Email == createStudentDTO.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var student = new Student
            {
                Name = createStudentDTO.Name,
                Email = createStudentDTO.Email,
                Phone = createStudentDTO.Phone
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone
            };
        }

        public async Task<bool> AssignCoursesToStudentAsync(int studentId, AssignCoursesDTO assignCourseDTO)
        {
            var student = await _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            var existingCourseIds = student.StudentCourses.Select(sc => sc.CourseId).ToList();
            var newCourseIds = assignCourseDTO.CourseIds.Except(existingCourseIds).ToList();

            if (!newCourseIds.Any())
            {
                throw new InvalidOperationException("No new courses to assign");
            }

            var courses = await _context.Courses
                .Where(c => newCourseIds.Contains(c.Id))
                .ToListAsync();

            if (courses.Count != newCourseIds.Count)
            {
                throw new KeyNotFoundException("One or more courses not found");
            }

            foreach (var course in courses)
            {
                student.StudentCourses.Add(new StudentCourse
                {
                    CourseId = course.Id,
                    StudentId = student.Id
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}