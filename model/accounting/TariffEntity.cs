namespace wsmcbl.back.model.accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string schoolYear { get; set; } = null!;
    public string concept { get; set; } = null!;
    public float amount { get; set; }
    public DateOnly? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }

    public void checkDueDate()
    {
        if (DateOnly.FromDateTime(DateTime.Today) >= dueDate)
            isLate = true;
    }
}