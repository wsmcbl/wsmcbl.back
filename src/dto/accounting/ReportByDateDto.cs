using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class ReportByDateDto
{
    public string userName { get; set; } = string.Empty;
    public DateTime consultedIn { get; set; } = DateTime.Now;
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public int validQuantity { get; set; }
    public double validTotal { get; set; }
    public int invalidQuantity { get; set; }
    public double invalidTotal { get; set; }

    public List<TransactionReportDto> transactionList { get; set; } = [];

    public void setTransactionList(IEnumerable<(TransactionEntity transaction, StudentEntity student)> list)
    {
        transactionList = list.mapToListDto();
    }

    public void setDateRage((DateTime start, DateTime end) value)
    {
        start = value.start;
        end = value.end;
    }

    public void setValidTransactionData((int quantity, double total) value)
    {
        validQuantity = value.quantity;
        validTotal = value.total;
    }

    public void setInvalidTransactionData((int quantity, double total) value)
    {
        invalidQuantity = value.quantity;
        invalidTotal = value.total;
    }
}