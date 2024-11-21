using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class CreateStudentProfileDtoValidator : AbstractValidator<CreateStudentProfileDto>
{
    public CreateStudentProfileDtoValidator()
    {
        RuleFor(d => d.educationalLevel)
            .GreaterThan(0)
            .LessThan(4)
            .WithMessage("EducationalLevel must be between 1 and 3");
        
        RuleFor(e => e.student)
            .SetValidator(new StudentToCreateDtoValidator());

        RuleFor(e => e.tutor)
            .SetValidator(new TutorToCreateDtoValidator());
    }
}