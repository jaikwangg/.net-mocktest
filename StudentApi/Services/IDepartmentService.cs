using StudentApi.Dtos;

namespace StudentApi.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentReadDto>> GetAllAsync();
        Task<DepartmentReadDto?> GetByIdAsync(int id);
        Task<DepartmentReadDto> CreateAsync(DepartmentCreateDto dto);
        Task<bool> UpdateAsync(int id, DepartmentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}