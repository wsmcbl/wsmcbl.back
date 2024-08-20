namespace wsmcbl.src.model.academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public int semesterId { get; set; }
    public int partial { get; set; }
    public DateOnly deadLine { get; set; }
    public string label { get; set; }

    public bool isClosed()
    {
        return deadLine < DateOnly.FromDateTime(DateTime.Today);
    }
    
    public ICollection<GradeEntity> grades { get; set; }

    public string getLabel()
    {
        return $"Parcial #{partial}";
    }
}