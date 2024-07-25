using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentTutorDto : IBaseDto<StudentTutorEntity>
{
    [Required] public string name { get; set; }
    [Required] public string phone { get; set; }
    
    public StudentTutorEntity toEntity()
    {
        return new StudentTutorEntity(name, phone);
    }
}