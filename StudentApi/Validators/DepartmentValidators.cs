using FluentValidation;
using StudentApi.Dtos;

namespace StudentApi.Validators
{
    public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
    {
        public DepartmentCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("ชื่อแผนกห้ามว่าง")
                .Must(n => !string.IsNullOrWhiteSpace(n)).WithMessage("ชื่อแผนกต้องไม่ประกอบด้วยช่องว่างเพียงอย่างเดียว")
                .MaximumLength(100).WithMessage("ชื่อแผนกต้องไม่เกิน 100 ตัวอักษร");
        }
    }
}