using FluentValidation;
using wsmcbl.src.dto.academy;

namespace wsmcbl.src.middleware.validator;

public class MoveTeacherDtoValidator : AbstractValidator<MoveTeacherDto>
{
    public MoveTeacherDtoValidator()
    {
        RuleFor(e => e.subjectId.Trim())
            .NotEmpty().WithMessage("Subject id must not be empty");
        
        RuleFor(e => e.enrollmentId.Trim())
            .NotEmpty().WithMessage("Enrollment id must not be empty");
        
        RuleFor(e => e.teacherId.Trim())
            .NotEmpty().WithMessage("Teacher id must not be empty");
    }
}