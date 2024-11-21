using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentParentDto : IBaseDto<StudentParentEntity>
{
    public string? parentId { get; set; }
    [JsonRequired] public bool sex { get; set; }
    [Required] public string name { get; set; } = null!;
    [Required] public string? idCard { get; set; }
    [Required] public string? occupation { get; set; }

    public StudentParentDto()
    {
    }

    public StudentParentDto(StudentParentEntity entity)
    {
        parentId = entity.parentId;
        sex = entity.sex;
        name = entity.name;
        idCard = entity.idCard;
        occupation = entity.occupation;
    }
    
    public StudentParentEntity toEntity()
    {
        return new StudentParentEntity
        {
            parentId =  !string.IsNullOrEmpty(parentId?.Trim()) ? parentId.Trim() : null,
            sex = sex,
            name = name.Trim(),
            idCard = idCard?.Trim(),
            occupation = occupation?.Trim(),
        };
    }
}