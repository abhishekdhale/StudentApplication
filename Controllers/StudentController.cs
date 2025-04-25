using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BasicApplication.DTOs;
using BasicApplication.Services;

namespace BasicApplication.Controllers
{
    [Authorize]
    [Route("api/student")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] CreateStudentDTO createStudentDTO)
        {
            try
            {
                var student = await _studentService.CreateStudentAsync(createStudentDTO);
                return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("{studentId}/courses")]
        public async Task<IActionResult> AssignCourses(int studentId, [FromBody] AssignCoursesDTO assignCoursesDTO)
        {
            try
            {
                await _studentService.AssignCoursesToStudentAsync(studentId, assignCoursesDTO);
                return Ok(new { message = "Courses assigned successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}