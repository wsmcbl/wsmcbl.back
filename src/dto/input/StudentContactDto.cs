using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class StudentContactDto : IBaseDto<StudentTutorEntity>
{
    [Required] public string name { get; set; }
    [Required] public string number { get; set; }
    
    public StudentTutorEntity toEntity()
    {
        return new StudentTutorEntity();
    }
}