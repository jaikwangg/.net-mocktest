using FluentValidation;
using StudentApi.Dtos;

namespace StudentApi.Validators
{
    public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("ชื่อวิชาห้ามว่าง")
                .Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("ชื่อวิชาต้องไม่ประกอบด้วยช่องว่างเพียงอย่างเดียว")
                .MaximumLength(100).WithMessage("ชื่อวิชาต้องไม่เกิน 100 ตัวอักษร");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("รายละเอียดต้องไม่เกิน 500 ตัวอักษร");

            RuleFor(x => x.Credits)
                .NotNull().WithMessage("กรุณาระบุหน่วยกิต")
                .InclusiveBetween(1, 5).WithMessage("หน่วยกิตต้องอยู่ระหว่าง 1 ถึง 5");
        }
    }
}