namespace wsmcbl.src.model.secretary;

public class TariffDataEntity
{
    public int tariffDataId { get; set; }
    public int typeId { get; set; } 
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnly? dueDate { get; set; }
    public int educationalLevel { get; set; }
    public bool isActive { get; set; }

    public void update(TariffDataEntity value)
    {
        typeId = value.typeId;
        concept = value.concept;
        amount = value.amount;
        dueDate = value.dueDate;
        educationalLevel = value.educationalLevel;
        isActive = value.isActive;
    }
}