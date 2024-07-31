using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentParentDto : IBaseDto<StudentParentEntity>
{
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 0)]
    public string? parentId { get; set; }
    
    [JsonRequired] public bool sex { get; set; }
    [Required] public string name { get; set; } = null!;
    
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 0)]
    public string? idCard { get; set; }
    
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 0)]
    public string? occupation { get; set; }

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
            parentId = parentId,
            sex = sex,
            name = name,
            idCard = idCard,
            occupation = occupation
        };
    }
}