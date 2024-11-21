using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class StudentParentDtoValidator : AbstractValidator<StudentParentDto>
{
    public StudentParentDtoValidator()
    {
        RuleFor(e => e.name.Trim())
            .NotNull().NotEmpty()
            .MinimumLength(3).WithMessage("Name be at least 3 characters long.");
        
        RuleFor(d => d.idCard)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .MinimumLength(3).WithMessage("IdCard be at least 3 characters long.");
        
        RuleFor(d => d.occupation)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .MinimumLength(3).WithMessage("Occupation be at least 3 characters long.");
    }
}