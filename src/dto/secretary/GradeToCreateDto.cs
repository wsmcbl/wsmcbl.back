using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class GradeToCreateDto : IBaseDto<GradeEntity>
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

    public GradeToCreateDto()
    {
    }

    public GradeToCreateDto(GradeEntity grade)
    {
        label = grade.label;
        modality = grade.modality;
        schoolYear = grade.schoolYear;
        subjects = grade.subjectList.Count == 0 ? [] : grade.subjectList.mapListToDto();
    }
}