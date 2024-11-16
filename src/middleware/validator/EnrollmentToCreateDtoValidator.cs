using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class EnrollmentToCreateDtoValidator : AbstractValidator<EnrollmentToCreateDto>
{
    public EnrollmentToCreateDtoValidator()
    {
        RuleFor(d => d.degreeId)
            .NotEmpty()
            .WithMessage("degreeId must not be empty");
        
        RuleFor(d => d.quantity)
            .GreaterThan(0)
            .LessThan(8)
            .WithMessage("Quantity must be between 1 and 7");
    }
}