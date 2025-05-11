using System.Text.Json.Serialization;

namespace wsmcbl.src.model.secretary;

public class SubjectDataEntity
{
    public int subjectDataId { get; set; }
    [JsonRequired] public int areaId { get; set; }
    [JsonRequired] public int degreeDataId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    [JsonRequired] public int number { get; set; }
    [JsonRequired] public bool isActive { get; set; }

    public void update(SubjectDataEntity value)
    {
        areaId = value.areaId;
        degreeDataId = value.degreeDataId;
        name = value.name;
        isMandatory = value.isMandatory;
        semester = value.semester;
        initials = value.initials;
        isActive = value.isActive;
        number = value.number;
    }
}