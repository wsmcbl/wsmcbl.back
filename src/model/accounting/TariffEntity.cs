using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string? schoolyearId { get; set; }
    public string concept { get; set; }  = null!;
    public decimal amount { get; set; }
    public DateOnly? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }
    public int educationalLevel { get; set; }

    public TariffEntity()
    {
    }
    
    public TariffEntity(TariffDataEntity tariffData, string schoolyearId)
    {
        this.schoolyearId = schoolyearId;
        type = tariffData.typeId;
        concept = tariffData.concept;
        amount = tariffData.amount;
        dueDate = tariffData.dueDate;
        educationalLevel = tariffData.educationalLevel;
    }

    public bool checkDueMonth(int month)
    {
        return dueDate != null && dueDate.Value.Month == month;
    }
    
    public void checkDueDate()
    {
        if (DateOnly.FromDateTime(DateTime.Today) >= dueDate)
        {
            isLate = true;
        }
    }

    public bool isMonthlyTariff()
    {
        return type == Const.TARIFF_MONTHLY;
    }
}