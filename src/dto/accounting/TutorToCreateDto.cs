using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.accounting;

public class TutorToCreateDto
{
    [Required] public string name { get; set; } = null!;
    [Required] public string phone { get; set; } = null!;

    public TutorToCreateDto()
    {
    }

    public TutorToCreateDto(StudentTutorEntity entity)
    {
        name = entity.name;
        phone = entity.phone;
    }

    public StudentTutorEntity toEntity()
    {
        return new StudentTutorEntity
        {
            tutorId = null,
            name = name,
            phone = phone
        };
    }
}