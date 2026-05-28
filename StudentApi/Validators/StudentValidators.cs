using FluentValidation;
using StudentApi.Dtos;
using StudentApi.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentApi.Validators
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        private readonly AppDbContext _context;

        public StudentCreateDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("ชื่อห้ามว่าง")
                .MinimumLength(2).WithMessage("ชื่อต้องมีอย่างน้อย 2 ตัวอักษร")
                .MaximumLength(100).WithMessage("ชื่อต้องไม่เกิน 100 ตัวอักษร")
                .Matches(@"^[a-zA-Zก-ฮะ-์\s]*$").WithMessage("ชื่อต้องเป็นตัวอักษรเท่านั้น")
                .Must(name => !name.StartsWith(" ") && !name.EndsWith(" ")).WithMessage("ชื่อห้ามมีช่องว่างนำหน้าหรือต่อท้าย")
                .MustAsync(BeUniqueName).WithMessage("ชื่อนักเรียนนี้มีอยู่ในระบบแล้ว");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("คะแนนต้องอยู่ระหว่าง 0 ถึง 100");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !await _context.Students.AnyAsync(s => s.Name == name, cancellationToken);
        }
    }

    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("ชื่อห้ามว่าง")
                .MinimumLength(2).WithMessage("ชื่อต้องมีอย่างน้อย 2 ตัวอักษร")
                .MaximumLength(100).WithMessage("ชื่อต้องไม่เกิน 100 ตัวอักษร")
                .Matches(@"^[a-zA-Zก-ฮะ-์\s]*$").WithMessage("ชื่อต้องเป็นตัวอักษรเท่านั้น")
                .Must(name => !name.StartsWith(" ") && !name.EndsWith(" ")).WithMessage("ชื่อห้ามมีช่องว่างนำหน้าหรือต่อท้าย");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("คะแนนต้องอยู่ระหว่าง 0 ถึง 100");
        }
    }
}