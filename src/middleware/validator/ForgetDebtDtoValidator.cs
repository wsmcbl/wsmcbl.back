using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.validator;

public class ForgetDebtDtoValidator : AbstractValidator<ForgetDebtDto>
{
    public ForgetDebtDtoValidator()
    {
        RuleFor(e => e.studentId)
            .NotEmpty()
            .WithMessage("studentId must not be empty");
        
        RuleFor(e => e.authorizationToken)
            .NotEmpty()
            .WithMessage("Token must not be empty");
        
        RuleFor(e => e.tariffId)
            .GreaterThan(0)
            .WithMessage("Tariff id must be not 0 or less.");
    }
}