using FluentValidation;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.middleware.validator;

public class SubjectDataEntityValidator : AbstractValidator<SubjectDataEntity>
{
    public SubjectDataEntityValidator()
    {
        RuleFor(d => d.areaId).GreaterThan(0).WithMessage("AreaId must be grater than 0.");
        RuleFor(d => d.degreeDataId).GreaterThan(0).WithMessage("DegreeDataId must be grater than 0.");
        RuleFor(d => d.semester).GreaterThan(0).WithMessage("Semester must be grater than 0.");
        RuleFor(d => d.number).GreaterThan(0).WithMessage("Number must be grater than 0.");

        RuleFor(e => e.name.Trim()).NotEmpty().WithMessage("Name must be not empty.");
        RuleFor(e => e.initials.Trim()).NotEmpty().WithMessage("Initials must be not empty.");
    }
}