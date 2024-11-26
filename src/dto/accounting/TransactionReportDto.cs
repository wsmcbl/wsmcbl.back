using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TransactionReportDto
{
    public string number { get; set; }
    public string studentName { get; set; }
    public DateTime datetime { get; set; }
    public double amount { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }
    
    public TransactionReportDto(TransactionReportView view)
    {
        number = view.number;
        datetime = view.dateTime;
        amount = view.total;
        type = view.type;
        studentName = view.studentName;
        isValid = view.isValid;
    }
}