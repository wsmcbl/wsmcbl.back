using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.filter;

public class EnrollmentToCreateDtoValidator : AbstractValidator<EnrollmentToCreateDto>
{
    public EnrollmentToCreateDtoValidator()
    {
        RuleFor(d => d.gradeId)
            .NotEmpty()
            .WithMessage("GradeId must not be empty");
        RuleFor(d => d.quantity)
            .GreaterThan(0)
            .LessThan(8)
            .WithMessage("Quantity must be between 1 and 7");
    }
}