using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class TutorToCreateDtoValidator : AbstractValidator<TutorToCreateDto>
{
    public TutorToCreateDtoValidator()
    {
        RuleFor(e => e.name)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second secondName must be not empty.")
            .MinimumLength(2).WithMessage("Student secondName be at least 2 characters long.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Student secondName must contain only letter");
    }
}