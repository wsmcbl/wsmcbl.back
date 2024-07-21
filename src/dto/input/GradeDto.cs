using System.ComponentModel.DataAnnotations;
using wsmcbl.src.dto.output;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class GradeDto : IBaseDto<GradeEntity>
{
    [Required] public string label { get; set; } = null!;
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string modality { get; set; } = null!;
    [Required] public List<SubjectDto>? subjects { get; set; }

    public GradeEntity toEntity()
    {
        var grade = new GradeEntity
        {
            label = label,
            schoolYear = schoolYear,
            modality = modality
        };

        var list = subjects!.Select(e => e.toEntity()).ToList();
        grade.setSubjectList(list);

        return grade;
    }

    public class Builder
    {
        private readonly GradeDto? dto;

        public Builder(GradeEntity grade)
        {
            dto = new GradeDto
            {
                label = grade.label,
                modality = grade.modality,
                schoolYear = grade.schoolYear,
                subjects = grade.subjectList.Count == 0 ? [] : grade.subjectList.mapListToDto()
            };
        }

        public GradeDto build() => dto!;
    }
}