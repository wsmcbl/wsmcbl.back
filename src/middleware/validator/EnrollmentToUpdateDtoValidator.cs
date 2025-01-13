using FluentValidation;
using wsmcbl.src.dto.academy;

namespace wsmcbl.src.middleware.validator;

public class EnrollmentToUpdateDtoValidator :  AbstractValidator<EnrollmentToUpdateDto>
{
    public EnrollmentToUpdateDtoValidator()
    {
        RuleFor(e => e.capacity)
            .GreaterThan(0).WithMessage("The capacity must be greater than zero.")
            .LessThan(60).WithMessage("The capacity must be less than 60.");
        
        RuleFor(e => e.quantity)
            .GreaterThan(-1).WithMessage("The quantity must be not negative.")
            .LessThan(60).WithMessage("The quantity must be less than 60.");

        RuleFor(e => e.section)
            .NotEmpty().WithMessage("The section label must be no empty.");
    }
}