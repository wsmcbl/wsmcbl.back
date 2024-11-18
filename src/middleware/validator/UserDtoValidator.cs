using FluentValidation;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.middleware.validator;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(e => e.name)
            .NotEmpty()
            .WithMessage("Name must not be empty");
        
        RuleFor(e => e.surname)
            .NotEmpty()
            .WithMessage("Surname must not be empty");
        
        RuleFor(e => e.email)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("The email must be the valid format");
        
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second surname must be not empty.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second surname must be not empty.");
    }
}