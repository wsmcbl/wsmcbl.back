using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class StudentFullDtoValidator : AbstractValidator<StudentFullDto>
{
    public StudentFullDtoValidator()
    {
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second name must be not empty.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second surname must be not empty.");
        
        RuleFor(e => e.parentList)
            .Must(list => list == null || list.Count > 0).WithMessage("The parent list must be not empty.")
            .ForEach(i => i.SetValidator(new StudentParentDtoValidator()));

        RuleFor(e => e.tutor)
            .SetValidator(new StudentTutorDtoValidator());
    }
}