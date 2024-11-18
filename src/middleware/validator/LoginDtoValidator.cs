using FluentValidation;
using wsmcbl.src.dto.config;

namespace wsmcbl.src.middleware.validator;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
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
    }
}