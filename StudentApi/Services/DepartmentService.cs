using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Dtos;

namespace StudentApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentReadDto>> GetAllAsync()
        {
            var items = await _context.Departments.ToListAsync();
            return items.Select(i => new DepartmentReadDto { Id = i.Id, Name = i.Name });
        }

        public async Task<DepartmentReadDto?> GetByIdAsync(int id)
        {
            var item = await _context.Departments.FindAsync(id);
            return item == null ? null : new DepartmentReadDto { Id = item.Id, Name = item.Name };
        }

        public async Task<DepartmentReadDto> CreateAsync(DepartmentCreateDto dto)
        {
            var item = new Department { Name = dto.Name };
            _context.Departments.Add(item);
            await _context.SaveChangesAsync();
            return new DepartmentReadDto { Id = item.Id, Name = item.Name };
        }

        public async Task<bool> UpdateAsync(int id, DepartmentCreateDto dto)
        {
            var item = await _context.Departments.FindAsync(id);
            if (item == null) return false;
            item.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Departments.FindAsync(id);
            if (item == null) return false;
            _context.Departments.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}