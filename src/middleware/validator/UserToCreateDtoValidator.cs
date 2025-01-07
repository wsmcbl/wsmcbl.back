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
        
        RuleFor(e => e.secondName)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second surname must be not empty.");
        
        RuleFor(e => e.secondSurname)
            .Must(e => e == null || !string.IsNullOrEmpty(e.Trim()))
            .WithMessage("The second surname must be not empty.");
    }
}