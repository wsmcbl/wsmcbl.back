using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectToCreateDto : IBaseDto<SubjectEntity>
{
    [Required] public string name { get; set; } = null!;
    [JsonRequired] public bool isMandatory { get; set; }
    [JsonRequired] public int semester { get; set; }
    [Required] public string initials { get; set; } = null!;
    [JsonRequired] public int area { get; set; }
    [JsonRequired] public int number { get; set; }

    public SubjectEntity toEntity()
    {
        return new SubjectEntity
        {
            name = name,
            isMandatory = isMandatory,
            semester = semester,
            initials = initials,
            areaId = area,
            number = number
        };
    }

    public SubjectToCreateDto()
    {
    }

    public SubjectToCreateDto(SubjectEntity subject)
    {
        name = subject.name;
        area = subject.areaId;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
        initials = subject.initials;
        number = subject.number;
    }
}