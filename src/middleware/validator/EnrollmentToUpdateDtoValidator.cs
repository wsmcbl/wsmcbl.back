using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class EnrollmentToUpdateDtoValidator :  AbstractValidator<EnrollmentToUpdateDto>
{
    public EnrollmentToUpdateDtoValidator()
    {
        RuleFor(e => e.capacity)
            .GreaterThan(0)
            .WithMessage("The capacity must be greater than zero.");
        
        RuleFor(e => e.quantity)
            .GreaterThan(-1)
            .WithMessage("The quantity must be not negative.");

        RuleFor(e => e.section)
            .NotEmpty()
            .WithMessage("The section label must be no empty.");
        
        RuleFor(e => e.subjectList)
            .NotEmpty()
            .WithMessage("Subject list must be not empty.");
    }
}