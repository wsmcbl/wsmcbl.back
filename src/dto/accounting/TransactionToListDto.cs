using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TransactionToListDto
{
    public string transactionId { get; set; }
    public string studentId { get; set; }
    public string studentName { get; set; }
    public string enrollmentLabel { get; set; }
    public double total { get; set; }
    public DateTime dateTime { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }
    
    public TransactionToListDto(TransactionReportView value)
    {
        transactionId = value.transactionId;
        studentId = value.studentId;
        studentName = value.studentName;
        enrollmentLabel = value.enrollmentLabel ??= "Sin matrícula";
        total = value.total;
        dateTime = value.dateTime;
        type = value.type;
        isValid = value.isValid;
    }
}