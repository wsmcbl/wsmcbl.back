namespace wsmcbl.src.model.secretary;

public class TariffDataEntity
{
    public int tariffDataId { get; set; }

    public string concept { get; set; } = null!;

    public double amount { get; set; }

    public DateOnly? dueDate { get; set; }

    public int typeId { get; set; }

    public int modality { get; set; }
}