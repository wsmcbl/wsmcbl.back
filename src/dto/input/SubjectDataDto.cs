using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SubjectDataDto : IBaseDto<SubjectDataEntity>
{
    [Required] public int gradeIntId { get; set; }
    [Required] public string name { get; set; }
    [Required] public bool isMandatory { get; set; }
    public int semester { get; set; }

    public SubjectDataEntity toEntity()
    {
        return new SubjectDataEntity
        {
            gradeDataId = gradeIntId,
            name = name,
            isMandatory = isMandatory
        };
    }
}