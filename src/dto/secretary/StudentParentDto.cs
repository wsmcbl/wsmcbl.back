using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentParentDto : IBaseDto<StudentParentEntity>
{
    [Required] public string? parentId { get; set; }
    [JsonRequired] public bool sex { get; set; }
    [Required] public string name { get; set; } = null!;
    [Required] public string address { get; set; } = null!;
    [Required] public string? idCard { get; set; }
    [Required] public string? phone { get; set; }
    [Required] public string? occupation { get; set; }

    public StudentParentDto()
    {
    }

    public StudentParentDto(StudentParentEntity entity)
    {
        parentId = entity.parentId;
        sex = entity.sex;
        name = entity.name;
        address = entity.address;
        idCard = entity.idCard;
        phone = entity.phone;
        occupation = entity.occupation;
    }
    
    public StudentParentEntity toEntity()
    {
        return new StudentParentEntity
        {
            parentId = parentId,
            sex = sex,
            name = name,
            address = address,
            idCard = idCard,
            phone = phone,
            occupation = occupation
        };
    }
}