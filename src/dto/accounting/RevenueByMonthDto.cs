using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class RevenueByMonthDto
{
    public AmountByStudentQuantityDto total { get; set; }
    public AmountByStudentQuantityDto pastMonths { get; set; }
    public AmountByStudentQuantityDto currentMonth { get; set; }
    public AmountByStudentQuantityDto futureMonths { get; set; }

    public RevenueByMonthDto(List<TransactionTariffView> parameter, DateTime startDate)
    {
        total = new AmountByStudentQuantityDto(parameter);
        
        var from = new DateOnly(startDate.Year, startDate.Month, startDate.Day);
        var to = from.AddMonths(1);

        var list = parameter.Where(e => e.tariffDueDate != null && e.tariffDueDate < from).ToList();
        pastMonths = new AmountByStudentQuantityDto(list);
        
        list = parameter.Where(e => e.tariffDueDate != null && e.tariffDueDate >= from && e.tariffDueDate < to).ToList();
        currentMonth = new AmountByStudentQuantityDto(list);
        
        list = parameter.Where(e => e.tariffDueDate != null && e.tariffDueDate >= to).ToList();
        futureMonths = new AmountByStudentQuantityDto(list);
    }
}