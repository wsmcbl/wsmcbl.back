using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class PartialToCreateDto
{
    public int partial { get; set; }
    public int semester { get; set; }
    public DateOnlyDto startDate { get; set; } = null!;
    public DateOnlyDto deadLine { get; set; } = null!;
    
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