using wsmcbl.src.exception;

namespace wsmcbl.src.model.academy;

public class SemesterEntity
{
    public int semesterId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public int semester { get; set; }
    public DateOnly? deadLine { get; set; }
    public bool isActive { get; set; }
    public string? label { get; set; }

    public List<PartialEntity>? partialList { get; set; }

    public void updateDeadLine()
    {
        var secondPartial = partialList!.FirstOrDefault(e => e.partial == 2);
        if (secondPartial == null)
        {
            throw new UpdateConflictException("Semester DeadLine", "The semester does not have a second partial.");
        }
        
        deadLine = secondPartial.deadLine;
    }

    public void updateLabel()
    {
        label = semester == 1 ? "I Semester" : "II Semester";
    }
}