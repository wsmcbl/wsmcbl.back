using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class RevenueByMonthDto
{
    public AmountByStudentQuantityDto total { get; set; }
    public AmountByStudentQuantityDto pastMonths { get; set; }
    public AmountByStudentQuantityDto currentMonth { get; set; }
    public AmountByStudentQuantityDto futureMonth { get; set; }

    public RevenueByMonthDto(List<TransactionTariffView> parameter)
    {
        total = new AmountByStudentQuantityDto(parameter);
    }
}