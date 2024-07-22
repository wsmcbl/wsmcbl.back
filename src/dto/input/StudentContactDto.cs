using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class StudentContactDto : IBaseDto<StudentContactEntity>
{
    [Required] public string name { get; set; }
    [Required] public string number { get; set; }
    
    public StudentContactEntity toEntity()
    {
        return new StudentContactEntity();
    }
}