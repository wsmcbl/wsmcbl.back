namespace wsmcbl.src.model.accounting;

public class TransactionTariffView
{
    public int tariffId { get; set; }
    public string studentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public int educationalLevel { get; set; }
    public DateTime transactionDate { get; set; }
    public DateOnly? tariffDueDate { get; set; }
    public decimal amount { get; set; }
    public int tariffType { get; set; }
}