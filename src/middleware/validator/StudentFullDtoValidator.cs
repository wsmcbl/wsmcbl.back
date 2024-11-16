using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class StudentFullDtoValidator : AbstractValidator<StudentFullDto>
{
    public StudentFullDtoValidator()
    {
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second name must be not empty.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second surname must be not empty.");
        
        RuleFor(e => e.parentList)
            .Must(list => list == null || list.Count > 0)
            .WithMessage("The parent list must be not empty.");
    }
}