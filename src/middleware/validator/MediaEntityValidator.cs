using FluentValidation;
using wsmcbl.src.model.config;

namespace wsmcbl.src.middleware.validator;

public class MediaEntityValidator : AbstractValidator<MediaEntity>
{
    public MediaEntityValidator()
    {
        RuleFor(e => e.type)
            .GreaterThan(0)
            .WithMessage("The type must be graater that zero");

        RuleFor(e => e.schoolyearId)
            .NotNull()
            .WithMessage("The schoolyearid must be not null")
            .NotEmpty()
            .WithMessage("The schoolyearid must be not empty");

        RuleFor(e => e.value)
            .NotNull()
            .WithMessage("The value must be not null")
            .NotEmpty()
            .WithMessage("The value must be not empty");

    }
}