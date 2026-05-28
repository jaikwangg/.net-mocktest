using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Dtos;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            return students.Select(s => MapToReadDto(s));
        }

        public async Task<StudentReadDto?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student == null ? null : MapToReadDto(student);
        }

        public async Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Score = studentDto.Score
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return MapToReadDto(student);
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentUpdateDto studentDto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            student.Name = studentDto.Name;
            student.Score = studentDto.Score;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        // Helper Method สำหรับแปลง Model -> DTO
        private StudentReadDto MapToReadDto(Student student)
        {
            return new StudentReadDto
            {
                Id = student.Id,
                Name = student.Name,
                Score = student.Score,
                Grade = student.Grade // คำนวณจาก Model อัตโนมัติ
            };
        }
    }
}