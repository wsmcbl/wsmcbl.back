using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SubjectDataDto
{
    public int subjectDataId {get; set;}
    [JsonRequired] public int gradeIntId { get; set; }
    [Required] public string name { get; set; } = null!;
    [JsonRequired] public bool isMandatory { get; set; }
    [JsonRequired] public int semester { get; set; }
    [Required] public string initials { get; set; } = null!;

    public SubjectDataEntity toEntity(int subjectId = 0)
    {
        return new SubjectDataEntity
        {
            subjectDataId = subjectId,
            degreeDataId = gradeIntId,
            name = name,
            isMandatory = isMandatory,
            semester = semester,
            initials = initials
        };
    }
}