namespace wsmcbl.src.model.accounting;

public class DebtEntity
{
    public string studentId { get; set; } = null!;

    public int tariffId { get; set; }

    public TariffEntity tariff { get; set; } = null!;

    public bool isPaid { get; set; }

    public string schoolyear { get; set; } = null!;
}
