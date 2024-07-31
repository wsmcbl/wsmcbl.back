using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectInputDto : IBaseDto<SubjectEntity>
{
    [Required] public string name { get; set; } = null!;
    [JsonRequired] public bool isMandatory { get; set; }
    [JsonRequired] public int semester { get; set; }

    public SubjectEntity toEntity()
    {
        return new SubjectEntity
        {
            name = name,
            isMandatory = isMandatory,
            semester = semester
        };
    }

    public SubjectInputDto()
    {
    }

    public SubjectInputDto(SubjectEntity subject)
    {
        name = subject.name;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
    }
}