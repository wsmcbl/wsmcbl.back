using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.accounting;

public class ReportByDateDto
{
    public string userName { get; set; } = string.Empty;
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public int validQuantity { get; set; }
    public double validTotal { get; set; }
    public int invalidQuantity { get; set; }
    public double invalidTotal { get; set; }
    public DateTime consultedIn { get; set; } = DateTime.UtcNow.toUTC6();

    public List<TransactionReportDto> transactionList { get; set; } = [];

    public void setTransactionList(IEnumerable<TransactionReportView> list)
    {
        transactionList = list.mapToListDto();
    }

    public void setDateRage(DateTime start, DateTime end)
    {
        startDate = start.toUTC6();
        endDate = end.toUTC6();
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