using StudentApi.Dtos;

namespace StudentApi.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseReadDto>> GetAllCoursesAsync();
        Task<CourseReadDto?> GetCourseByIdAsync(int id);
        Task<CourseReadDto> CreateCourseAsync(CourseCreateDto courseDto);
        Task<bool> UpdateCourseAsync(int id, CourseCreateDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}