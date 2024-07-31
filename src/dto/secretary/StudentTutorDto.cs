using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentTutorDto : IBaseDto<StudentTutorEntity>
{
    public string? tutorId { get; set; }
    [Required] public string name { get; set; } = null!;
    [Required] public string phone { get; set; } = null!;
    
    public StudentTutorDto()
    {
    }

    public StudentTutorDto(StudentTutorEntity entity)
    {
        tutorId = entity.tutorId;
        name = entity.name;
        phone = entity.phone;
    }
    
    public StudentTutorEntity toEntity()
    {
        return new StudentTutorEntity(tutorId, name, phone);
    }
}