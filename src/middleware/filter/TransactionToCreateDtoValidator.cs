using FluentValidation;
using wsmcbl.src.dto.accounting;

namespace wsmcbl.src.middleware.filter;

public class TransactionToCreateDtoValidator : AbstractValidator<TransactionDto>
{
    public TransactionToCreateDtoValidator()
    {
        RuleFor(d => d.studentId)
            .NotEmpty()
            .WithMessage("StudentId must not be empty");
        RuleFor(d => d.cashierId)
            .NotEmpty()
            .WithMessage("CashierId must not be empty");
        RuleFor(d => d.details.Count)
            .GreaterThan(0)
            .WithMessage("Detail must not be empty");
        RuleFor(d => d.details)
            .ForEach(i => i.SetValidator(new DetailDtoValidator()));
    }
}