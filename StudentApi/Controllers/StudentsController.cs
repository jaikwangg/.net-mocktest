using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentApi.Dtos;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ต้อง Login ก่อนถึงจะใช้ Controller นี้ได้
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // เฉพาะ Admin เท่านั้นที่เพิ่มข้อมูลได้
        public async Task<ActionResult<StudentReadDto>> PostStudent(StudentCreateDto studentDto)
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutStudent(int id, StudentUpdateDto studentDto)
        {
            var success = await _studentService.UpdateStudentAsync(id, studentDto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // --- New LINQ Endpoints ---

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> SearchStudents([FromQuery] string name)
        {
            var results = await _studentService.SearchByNameAsync(name);
            return Ok(results);
        }

        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetTopPerformers([FromQuery] int count = 3)
        {
            var results = await _studentService.GetTopPerformersAsync(count);
            return Ok(results);
        }

        [HttpGet("stats")]
        [AllowAnonymous] // ให้ทุกคนดูสถิติได้
        public async Task<ActionResult<object>> GetStats()
        {
            var stats = await _studentService.GetGradeStatisticsAsync();
            return Ok(stats);
        }
    }
}