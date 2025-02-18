using FluentValidation;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.middleware.validator;

public class PagedRequestValidator : AbstractValidator<PagedRequest>
{
    public PagedRequestValidator()
    {
        RuleFor(e => e.page)
            .GreaterThan(0).WithMessage("The page must be greater than zero.");
        
        RuleFor(e => e.pageSize)
            .GreaterThan(9).WithMessage("The pageSize must be equal or greater than 10.")
            .LessThan(201).WithMessage("The pageSize must be equal or less than 200.");
    }
}