using FluentValidation;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.src.middleware.filter;

public class StudentParentDtoValidator : AbstractValidator<StudentParentDto>
{
    public StudentParentDtoValidator()
    {
        RuleFor(d => d.parentId)
            .NotNull()
            .WithMessage("ParentId must not be null");
    }
}