using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.filter;

public class SchoolyearToCreateDtoValidator : AbstractValidator<SchoolYearToCreateDto>
{
    public SchoolyearToCreateDtoValidator()
    {
        RuleFor(e => e.exchangeRate)
            .GreaterThan(0)
            .WithMessage("Exchange rate must be greater tha zero.");
    }
}