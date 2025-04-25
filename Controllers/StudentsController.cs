using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            try
            {
                var response = await _studentService.CreateStudentAsync(request);
                return CreatedAtAction(nameof(GetAllStudents), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the student");
            }
        }

        [HttpPost("{id}/courses")]
        public async Task<IActionResult> AssignCourses(int id, [FromBody] AssignCoursesRequest request)
        {
            try
            {
                await _studentService.AssignCoursesAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while assigning courses");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving students");
            }
        }
    }
} 