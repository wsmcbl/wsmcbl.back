namespace wsmcbl.src.model.academy;

public class SemesterEntity
{
    public int semesterId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public int semester { get; set; }
    public DateOnly? deadLine { get; set; }
    public bool isActive { get; set; }
    public string? label { get; set; }

    public ICollection<PartialEntity>? partialList { get; set; }

    public void updateDeadLine()
    {
        var secondPartial = partialList!.First(e => e.partial == 2);
        deadLine = secondPartial.deadLine;
    }
}