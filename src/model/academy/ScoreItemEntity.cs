namespace wsmcbl.src.model.academy;

public class ScoreItemEntity
{
    public int itemId { get; set; }
    public int scoreId { get; set; }
    public int partial { get; set; }
    public double? score { get; set; }
    public string? label { get; set; }
}