using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Dtos;

namespace StudentApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseReadDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses.Select(c => new CourseReadDto { 
                Id = c.Id, Title = c.Title, Description = c.Description, Credits = c.Credits 
            });
        }

        public async Task<CourseReadDto?> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return null;
            return new CourseReadDto { 
                Id = course.Id, Title = course.Title, Description = course.Description, Credits = course.Credits 
            };
        }

        public async Task<CourseReadDto> CreateCourseAsync(CourseCreateDto courseDto)
        {
            var course = new Course { 
                Title = courseDto.Title, Description = courseDto.Description, Credits = courseDto.Credits 
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return new CourseReadDto { 
                Id = course.Id, Title = course.Title, Description = course.Description, Credits = course.Credits 
            };
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseCreateDto courseDto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Credits = courseDto.Credits;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}