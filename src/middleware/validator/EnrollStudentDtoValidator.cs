using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class EnrollStudentDtoValidator :  AbstractValidator<EnrollStudentDto>
{
    public EnrollStudentDtoValidator()
    {
        RuleFor(d => d.discountId)
            .GreaterThan(0)
            .WithMessage("Quantity must be grater that zero.");
        
        RuleFor(e => e.student)
            .SetValidator(new StudentFullDtoValidator());
    }
}