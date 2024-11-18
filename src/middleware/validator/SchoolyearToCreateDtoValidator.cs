using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class SchoolyearToCreateDtoValidator : AbstractValidator<SchoolYearToCreateDto>
{
    public SchoolyearToCreateDtoValidator()
    {
        RuleFor(e => e.exchangeRate)
            .GreaterThan(0)
            .WithMessage("Exchange rate must be greater tha zero.");
        
        RuleFor(e => e.degreeList)
            .NotEmpty()
            .WithMessage("DegreeList must be not empty.");
        
        RuleFor(e => e.tariffList)
            .NotEmpty()
            .WithMessage("TariffList must be not empty.");
        
        RuleFor(e => e.partialList)
            .NotEmpty()
            .WithMessage("PartialList must be not empty.");
    }
}