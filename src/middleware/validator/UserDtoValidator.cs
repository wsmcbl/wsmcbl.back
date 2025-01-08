using FluentValidation;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.middleware.validator;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(e => e.name.Trim())
            .NotEmpty().WithMessage("Name must not be empty")
            .MinimumLength(3).WithMessage("Name be at least 3 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ]+$").WithMessage("Name must contain only letter");
        
        RuleFor(e => e.surname.Trim())
            .NotEmpty().WithMessage("Surname must not be empty")
            .MinimumLength(2).WithMessage("Name be at least 2 characters long.")
            .Matches(@"^[a-zA-ZÀ-ÿñÑ]+$").WithMessage("Name must contain only letter");
        
        RuleFor(e => e.email)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("The email must be the valid format");
        
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second surname must be not empty.")
            .MinimumLength(2).WithMessage("Name be at least 2 characters long.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second surname must be not empty.")
            .MinimumLength(2).WithMessage("Name be at least 2 characters long.");
    }
}