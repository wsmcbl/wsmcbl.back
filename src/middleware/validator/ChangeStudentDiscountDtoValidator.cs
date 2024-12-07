using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class ChangeStudentDiscountDtoValidator : AbstractValidator<ChangeStudentDiscountDto>
{
    public ChangeStudentDiscountDtoValidator()
    {
        RuleFor(d => d.discountId)
            .GreaterThan(0)
            .LessThan(4)
            .WithMessage("DiscountId must be between 1 and 3");
    }
}