using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Dtos;
using AutoMapper;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            return _mapper.Map<IEnumerable<StudentReadDto>>(students);
        }

        public async Task<StudentReadDto?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return _mapper.Map<StudentReadDto>(student);
        }

        public async Task<StudentReadDto> CreateStudentAsync(StudentCreateDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentReadDto>(student);
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentUpdateDto studentDto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _mapper.Map(studentDto, student);

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

        // --- LINQ Examples ---

        // ค้นหาตามชื่อ (ใช้ Where, Contains)
        public async Task<IEnumerable<StudentReadDto>> SearchByNameAsync(string name)
        {
            var students = await _context.Students
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(s => s.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StudentReadDto>>(students);
        }

        // ดึงนักเรียนที่คะแนนสูงสุด X อันดับแรก (ใช้ OrderByDescending, Take)
        public async Task<IEnumerable<StudentReadDto>> GetTopPerformersAsync(int count)
        {
            var students = await _context.Students
                .OrderByDescending(s => s.Score)
                .ThenBy(s => s.Name)
                .Take(count)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StudentReadDto>>(students);
        }

        // รายงานสถิติเกรด (ใช้ GroupBy, Select, Count, Average)
        public async Task<object> GetGradeStatisticsAsync()
        {
            var students = await _context.Students.ToListAsync(); // โหลดมาคำนวณ Grade ใน Memory เพราะ Grade เป็น Calculated Property ใน Model
            
            var stats = students
                .GroupBy(s => s.Grade)
                .Select(g => new
                {
                    Grade = g.Key,
                    Count = g.Count(),
                    AverageScore = g.Average(s => s.Score),
                    Students = g.Select(s => s.Name).ToList()
                })
                .OrderBy(g => g.Grade);

            return new
            {
                TotalStudents = students.Count,
                AverageTotalScore = students.Any() ? students.Average(s => s.Score) : 0,
                GradeDistribution = stats
            };
        }
    }
}