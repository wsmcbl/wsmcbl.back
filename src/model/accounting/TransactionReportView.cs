namespace wsmcbl.src.model.accounting;

public class TransactionReportView
{
    public string transactionid { get; set; } = null!;
    public int number { get; set; }
    public string studentid { get; set; } = null!;
    public string studentname { get; set; } = null!;
    public double total { get; set; }
    public bool isvalid { get; set; }
    public string? enrollmentlabel { get; set; }
    public int type { get; set; }
    public DateTime datetime { get; set; }
}