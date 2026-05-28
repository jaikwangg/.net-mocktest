using FluentValidation;
using StudentApi.Dtos;

namespace StudentApi.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("กรุณากรอก Username")
                .MinimumLength(3).WithMessage("Username ต้องมีอย่างน้อย 3 ตัวอักษร")
                .Matches(@"^[a-zA-Z0-9_]*$").WithMessage("Username ต้องประกอบด้วยตัวอักษร ตัวเลข หรือ underscore เท่านั้น");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("กรุณากรอก Password")
                .MinimumLength(6).WithMessage("Password ต้องมีอย่างน้อย 6 ตัวอักษร")
                .Matches(@"[A-Z]").WithMessage("Password ต้องมีตัวอักษรตัวใหญ่ย่างน้อย 1 ตัว")
                .Matches(@"[a-z]").WithMessage("Password ต้องมีตัวอักษรตัวเล็กอย่างน้อย 1 ตัว")
                .Matches(@"[0-9]").WithMessage("Password ต้องมีตัวเลขอย่างน้อย 1 ตัว");
        }
    }
}