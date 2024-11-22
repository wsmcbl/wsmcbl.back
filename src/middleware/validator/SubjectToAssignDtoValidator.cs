using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class SubjectToAssignDtoValidator : AbstractValidator<SubjectToAssignDto>
{
    public SubjectToAssignDtoValidator()
    {
        RuleFor(e => e.teacherId)
            .NotNull().WithMessage("Id must be not null.")
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("Id must be not empty.")
            .MinimumLength(7).WithMessage("Id must be in the correct format.");
    }
}