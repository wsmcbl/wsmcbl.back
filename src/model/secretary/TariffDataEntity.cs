namespace wsmcbl.src.model.secretary;

public class TariffDataEntity
{
    public int tariffDataId { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; } 
    public string concept { get; set; } = null!;
    public float amount { get; set; }
    public DateOnly? dueDate { get; set; }
    public int modality { get; set; }
}