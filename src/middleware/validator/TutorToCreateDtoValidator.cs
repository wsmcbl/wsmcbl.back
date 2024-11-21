using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class TutorToCreateDtoValidator : AbstractValidator<TutorToCreateDto>
{
    public TutorToCreateDtoValidator()
    {
        RuleFor(e => e.name)
            .NotNull().NotEmpty().WithMessage("The name must be not null or empty.")
            .MinimumLength(3).WithMessage("Name be at least 3 characters long.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name must contain only letters");
        
        RuleFor(e => e.phone)
            .NotNull().NotEmpty().WithMessage("The phone must be not null or empty.")
            .MinimumLength(3).WithMessage("Phone be at least 3 characters long.")
            .Matches(@"^(N\/A|(\d{8})(,\s*\d{8})*)$").WithMessage("Name must contain only letters");
    }
}