using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class StudentTutorDtoValidator : AbstractValidator<StudentTutorDto>
{
    public StudentTutorDtoValidator()
    {
        RuleFor(e => e.name)
            .NotNull().NotEmpty().WithMessage("The name must be not null or empty.")
            .MinimumLength(3).WithMessage("Name be at least 3 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ\s]+$").WithMessage("Name must contain only letters");
        
        RuleFor(e => e.phone)
            .NotNull().NotEmpty().WithMessage("The phone must be not null or empty.")
            .MinimumLength(3).WithMessage("Phone be at least 3 characters long.")
            .Matches(@"^(N\/A|(\d{8})(,\s*\d{8})*)$")
            .WithMessage("The phone number must be a valid number, it can be several.");
        
        RuleFor(e => e.email)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim())).WithMessage("The email must be not empty.")
            .Matches(@"(N\/A|^[^@\s]+@[^@\s]+\.[^@\s]+$)").WithMessage("The email must be the valid format.");
    }
}