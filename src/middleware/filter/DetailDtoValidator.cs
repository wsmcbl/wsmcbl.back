using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.filter;

internal class DetailDtoValidator : AbstractValidator<DetailDto>
{
    public DetailDtoValidator()
    {
        RuleFor(d => d.tariffId)
            .GreaterThan(0)
            .WithMessage("TariffId invalid");
        RuleFor(d => d.amount)
            .GreaterThan(0)
            .WithMessage("Amount invalid");
    }
}