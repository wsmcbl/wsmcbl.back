namespace wsmcbl.src.model.accounting;

public class ExchangeRateEntity
{
    public int rateId { get; set; }
    public string schoolyear { get; set; } = null!;
    public double value { get; set; }
}