using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class ScoreDto
{
    public double score { get; set; }
    public string label { get; set; }
    public string initilas { get; set; }
    public List<ScoreItemDto> items { get; set; }

    public ScoreDto()
    {
    }

    public ScoreDto(ScoreEntity entity)
    {
        score = (double)entity.score!;
        label = entity.label;
        initilas = entity.getInitials();

        items = [];
        foreach (var item in entity.items)
        {
            items.Add(new ScoreItemDto(item));
        }
    }
}