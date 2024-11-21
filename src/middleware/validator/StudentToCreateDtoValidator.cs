using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class StudentToCreateDtoValidator : AbstractValidator<StudentToCreateDto>
{
    public StudentToCreateDtoValidator()
    {
        RuleFor(e => e.name.Trim())
            .NotEmpty().WithMessage("Student name must be not empty")
            .MinimumLength(3).WithMessage("Student name be at least 3 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ]+$").WithMessage("Student name must contain only letter");

        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second secondName must be not empty.")
            .MinimumLength(2).WithMessage("Student secondName be at least 2 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ\s]+$").WithMessage("Student secondName must contain only letter");

        RuleFor(e => e.surname.Trim())
            .NotEmpty().WithMessage("Student surname must be not empty")
            .MinimumLength(2).WithMessage("Student surname be at least 2 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ]+$").WithMessage("Student surname must contain only letter");

        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second secondSurname must be not empty.")
            .MinimumLength(2).WithMessage("Student secondSurname be at least 2 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ\s]+$").WithMessage("Student secondSurname must contain only letter");
    }
}