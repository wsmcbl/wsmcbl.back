using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SubjectDto : IBaseDto<SubjectEntity>
{
    [Required] public string name { get; set; }
    public bool isMandatory { get; set; }
    
    public SubjectEntity toEntity()
    {
        return new SubjectEntity
        {
            name = name,
            isMandatory = isMandatory
        };
    }
}