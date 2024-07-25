using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentParentDto : IBaseDto<StudentParentEntity>
{
    [JsonRequired] public bool sex { get; set; }
    [Required] public string name { get; set; }
    [Required] public string address { get; set; }
    [Required] public string? idCard { get; set; }
    [Required] public string? phone { get; set; }
    [Required] public string? occupation { get; set; }
    
    public StudentParentEntity toEntity()
    {
        return new StudentParentEntity
        {
            sex = sex,
            name = name,
            address = address,
            idCard = idCard,
            phone = phone,
            occupation = occupation
        };
    }
}