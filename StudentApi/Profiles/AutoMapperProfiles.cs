using AutoMapper;
using StudentApi.Dtos;
using StudentApi.Models;

namespace StudentApi.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Source -> Destination
            CreateMap<Student, StudentReadDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
        }
    }

    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseReadDto>();
            CreateMap<CourseCreateDto, Course>();
        }
    }

    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentReadDto>();
            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}