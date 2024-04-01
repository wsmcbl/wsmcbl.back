namespace wsmcbl.back.model.accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string concept { get; set; } = null!;
    public float amount { get; set; }
    public ICollection<TransactionEntity> transactions { get; set; }
}