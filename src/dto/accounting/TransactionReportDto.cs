using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TransactionReportDto
{
    public int number { get; set; }
    public string studentName { get; set; }
    public string enrollmentName { get; set; }
    public DateTime datetime { get; set; }
    public double amount { get; set; }
    public int type { get; set; }
    
    public TransactionReportDto(TransactionEntity transaction, StudentEntity student)
    {
        number = transaction.number;
        datetime = transaction.date;
        amount = transaction.total;
        type = transaction.getTariffPaidType();
        studentName = student.fullName();
        enrollmentName = student.enrollmentLabel!;
    }
}