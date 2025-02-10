namespace wsmcbl.src.model.accounting;

public class TransactionInvoiceView
{
    public string transactionId { get; set; } = null!;
    public int number { get; set; }
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public double total { get; set; }
    public bool isValid { get; set; }
    public string concept { get; set; } = null!;
    public string cashier { get; set; } = null!;
    public DateTime dateTime { get; set; }
}