namespace wsmcbl.src.model.accounting;

public class TransactionReportView
{
    public int transactionId { get; set; }
    public string number { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public double total { get; set; }
    public bool isValid { get; set; }
    public string? label { get; set; }
    public int type { get; set; }
    public DateTime dateTime { get; set; }
}