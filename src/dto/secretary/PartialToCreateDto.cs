using System.Text.Json.Serialization;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class PartialToCreateDto
{
    [JsonRequired] public int partial { get; set; }
    [JsonRequired] public int semester { get; set; }
    [JsonRequired] public DateOnlyDto startDate { get; set; } = null!;
    [JsonRequired] public DateOnlyDto deadLine { get; set; } = null!;
    
    public PartialEntity toEntity()
    {
        var result = new PartialEntity
        {
            partial = partial,
            semester = semester,
            startDate = startDate.toEntity(),
            deadLine = deadLine.toEntity(),
            isActive = false,
            gradeRecordIsActive = false
        };
        result.updateLabel();

        return result;
    }
}