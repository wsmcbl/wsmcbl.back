namespace wsmcbl.src.model.accounting;

public class TransactionReportView
{
    public string transactionId { get; set; } = null!;
    public int number { get; set; }
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public decimal total { get; set; }
    public bool isValid { get; set; }
    public string? enrollmentLabel { get; set; }
    public int type { get; set; }
    public DateTime dateTime { get; set; }
}