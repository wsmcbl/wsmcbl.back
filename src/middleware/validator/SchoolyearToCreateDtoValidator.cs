using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class SchoolyearToCreateDtoValidator : AbstractValidator<SchoolyearToCreateDto>
{
    public SchoolyearToCreateDtoValidator()
    {
        RuleFor(e => e.tariffList)
            .NotEmpty()
            .WithMessage("TariffList must be not empty.");
        
        RuleFor(e => e.partialList)
            .NotEmpty()
            .WithMessage("PartialList must be not empty.");
    }
}