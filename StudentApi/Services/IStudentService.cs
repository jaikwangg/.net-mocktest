using StudentApi.Dtos;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync();
        Task<StudentReadDto?> GetStudentByIdAsync(int id);
        Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentDto);
        Task<bool> UpdateStudentAsync(int id, StudentUpdateDto studentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}