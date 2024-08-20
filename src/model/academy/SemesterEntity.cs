namespace wsmcbl.src.model.academy;

public class SemesterEntity
{
    public int semesterId { get; set; }
    public string schoolyear { get; set; } = null!;
    public int semester { get; set; }
    public DateOnly? deadLine { get; set; }
    public bool isActive { get; set; }
    public string? label { get; set; }

    public ICollection<PartialEntity> partials { get; set; }
}