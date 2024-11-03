using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class PartialToCreateDto
{
    public int partial { get; set; }
    public int semester { get; set; }
    public DateOnlyDto deadLine { get; set; } = null!;
    
    public PartialEntity toEntity()
    {
        var result = new PartialEntity
        {
            partial = partial,
            semester = semester,
            deadLine = deadLine.toEntity()
        };
        
        result.updateLabel();

        return result;
    }
}