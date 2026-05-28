using Microsoft.AspNetCore.Mvc;
using StudentApi.Dtos;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses() => 
            Ok(await _courseService.GetAllCoursesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            return course != null ? Ok(course) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> PostCourse(CourseCreateDto courseDto)
        {
            var createdCourse = await _courseService.CreateCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseCreateDto courseDto)
        {
            var success = await _courseService.UpdateCourseAsync(id, courseDto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}