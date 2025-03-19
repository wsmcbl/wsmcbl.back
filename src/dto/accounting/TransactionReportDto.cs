using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.accounting;

public class TransactionReportDto
{
    public int number { get; set; }
    public string studentName { get; set; }
    public string datetime { get; set; }
    public decimal amount { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }
    
    public TransactionReportDto(TransactionReportView view)
    {
        number = view.number;
        datetime = view.dateTime.toStringUtc6();
        amount = view.total;
        type = view.type;
        studentName = view.studentName;
        isValid = view.isValid;
    }
}