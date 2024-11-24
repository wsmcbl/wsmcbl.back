namespace wsmcbl.src.dto.accounting;

public class ReportByDateDto
{
    public string userName { get; set; } = null!;
    public DateTime consultedIn { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public int totalQuantity { get; set; }
    public double totalAmount { get; set; }
    public int validQuantity { get; set; }
    public double validAmount { get; set; }

    public List<TransactionReportDto> transactionList { get; set; } = null!;
}