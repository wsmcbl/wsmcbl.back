namespace wsmcbl.src.model.academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public int semesterId { get; set; }
    public int partial { get; set; }
    public DateOnly? deadLine { get; set; }

    public bool isClosed()
    {
        var result = ((DateOnly)deadLine!).CompareTo(DateTime.Today.Date);

        return result < 0;
    }
    
    public ICollection<GradeEntity> grades { get; set; }

    public string getLabel()
    {
        return $"Parcial #{partial}";
    }
}