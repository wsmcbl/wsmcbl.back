using wsmcbl.src.model;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.accounting;

public class PagedReportByDateDto : PagedResult<TransactionReportDto>
{
    public ReportByDateDto summary { get; set; } = null!;

    public PagedReportByDateDto(List<TransactionReportDto> list) : base(list)
    {
    }

    public void setDateRange(DateTime start, DateTime end)
    {
        summary.startDate = start.toUTC6();
        summary.endDate = end.toUTC6();
    }

    public void setValidTransactionData((int quantity, double total) value)
    {
        summary.validQuantity = value.quantity;
        summary.validTotal = value.total;
    }

    public void setInvalidTransactionData((int quantity, double total) value)
    {
        summary.invalidQuantity = value.quantity;
        summary.invalidTotal = value.total;
    }

    public void setUserName(string value)
    {
        summary.userName = value;
    }
}