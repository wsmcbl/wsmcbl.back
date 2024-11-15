using FluentValidation;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.middleware.validator;

public class UserToCreateDtoValidator : AbstractValidator<UserToCreateDto>
{
    public UserToCreateDtoValidator()
    {
        RuleFor(e => e.roleId)
            .GreaterThan(0)
            .WithMessage("RoleId must be a positive integer.");
        
        RuleFor(e => e.name)
            .NotEmpty()
            .WithMessage("Name must not be empty");
        
        RuleFor(e => e.surname)
            .NotEmpty()
            .WithMessage("Surname must not be empty");
        
        RuleFor(e => e.email)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("The email must be the valid format");
        
        RuleFor(e => e.password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one digit")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]")
            .WithMessage("Password must contain at least one special character (!@#$%^&*()-+=)");
        
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second surname must be not empty.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrWhiteSpace(e))
            .WithMessage("The second surname must be not empty.");
    }
}