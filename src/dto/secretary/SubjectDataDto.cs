using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectDataDto : IBaseDto<SubjectDataEntity>
{
    [JsonRequired] public int gradeIntId { get; set; }
    [Required] public string name { get; set; } = null!;
    [JsonRequired] public bool isMandatory { get; set; }
    [JsonRequired] public int semester { get; set; }
    [Required] public string initials { get; set; }

    public SubjectDataEntity toEntity()
    {
        return new SubjectDataEntity
        {
            degreeDataId = gradeIntId,
            name = name,
            isMandatory = isMandatory,
            semester = semester,
            initials = initials
        };
    }
}