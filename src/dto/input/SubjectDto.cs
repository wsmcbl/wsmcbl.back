using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SubjectDto : IBaseDto<SubjectEntity>
{
    public int gradeId { get; set; }
    [Required] public string name { get; set; }
    public bool isMandatory { get; set; }
    
    public SubjectEntity toEntity()
    {
        return new SubjectEntity
        {
            gradeId = gradeId,
            name = name,
            isMandatory = isMandatory
        };
    }
}