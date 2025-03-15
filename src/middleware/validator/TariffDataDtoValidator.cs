using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.validator;

public class TariffDataDtoValidator : AbstractValidator<TariffDataDto>
{
    public TariffDataDtoValidator()
    {
        RuleFor(d => d.typeId).GreaterThan(0).WithMessage("TypeId must be grater than 0.");
        RuleFor(d => d.educationalLevel).GreaterThan(0).WithMessage("EducationalLevel must be grater than 0.");

        RuleFor(e => e.concept.Trim()).NotEmpty().WithMessage("Concept must be not empty.");
    }
}