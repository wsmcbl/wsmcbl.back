namespace wsmcbl.src.model.accounting;

public class TransactionTariffView
{
    public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public int tariffType { get; set; }
    public decimal amount { get; set; }
    public int educationalLevel { get; set; }
    public DateTime transactionDate { get; set; }
    public DateOnly? tariffDueDate { get; set; }
}