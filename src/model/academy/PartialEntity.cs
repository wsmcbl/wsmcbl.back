namespace wsmcbl.src.model.academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public int semesterId { get; set; }
    public int partial { get; set; }
    public int semester { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public string label { get; set; } = null!;
    
    public bool isActive { get; set; }
    public bool gradeRecordIsActive { get; set; }

    public bool isClosed()
    {
        return deadLine < DateOnly.FromDateTime(DateTime.Today);
    }
    
    public ICollection<GradeEntity>? grades { get; set; }

    public void updateLabel()
    {
        if (semester == 1)
        {
            label = partial == 1 ? "I Parcial" : "II Parcial";
        }
        else
        {
            label = partial == 2 ? "II Parcial" : "IV Parcial";
        }
    }
}