using System.ComponentModel.DataAnnotations;
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
}