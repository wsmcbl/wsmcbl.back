namespace wsmcbl.src.model.academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public int semesterId { get; set; }
    public int partial { get; set; }
    public DateOnly? deadLine { get; set; }
    
    public virtual ICollection<GradeEntity> grades { get; set; } = new List<GradeEntity>();
}