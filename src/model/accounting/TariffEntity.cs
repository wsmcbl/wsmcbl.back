using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string? schoolYear { get; set; }
    public string concept { get; set; }  = null!;
    public float amount { get; set; }
    public DateOnly? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }
    public int modality { get; set; }

    public TariffEntity()
    {
    }
    
    public TariffEntity(TariffDataEntity tariffData, string schoolYear)
    {
        this.schoolYear = schoolYear;
        concept = tariffData.concept;
        amount = tariffData.amount;
        dueDate = tariffData.dueDate;
        type = tariffData.typeId;
        modality = tariffData.modality;
    }
    
    public void checkDueDate()
    {
        if (DateOnly.FromDateTime(DateTime.Today) >= dueDate)
        {
            isLate = true;
        }
    }
}