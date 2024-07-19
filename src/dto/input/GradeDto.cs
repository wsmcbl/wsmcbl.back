using System.ComponentModel.DataAnnotations;
using wsmcbl.src.dto.output;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class GradeDto : IBaseDto<GradeEntity>
{
    [Required] public string label { get; set; }
    [Required] public string schoolYear { get; set; }
    [Required] public string modality { get; set; }
    
    public List<SubjectDto> subjects { get; set; }
    
    public GradeEntity toEntity()
    {
        var grade = new GradeEntity
        {
            label = label,
            schoolYear = schoolYear,
            modality = modality
        };

        var list = subjects.Select(e => e.toEntity()).ToList();
        grade.setSubjectList(list);

        return grade;
    }

    internal static GradeDto init(GradeEntity grade)
    {
        return new GradeDto
        {
            label = grade.label,
            modality = grade.modality,
            schoolYear = grade.schoolYear,
            subjects = grade.subjectList.Count == 0 ? [] : grade.subjectList.mapListToDto()
        };
    }
}