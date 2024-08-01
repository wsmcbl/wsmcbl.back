using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class ScoreItemDto
{
    public double score { get; set; }
    public string label { get; set; }
    public int partial { get; set; }
    
    public ScoreItemDto()
    {
    }
    
    public ScoreItemDto(ScoreItemEntity item)
    {
        score = (double)item.score!;
        label = item.label!;
        partial = item.partial;
    }
}