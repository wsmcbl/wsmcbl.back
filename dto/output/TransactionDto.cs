using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class TransactionDto
{
    public TransactionDto(TransactionEntity transaction, StudentEntity student, CashierEntity cashier)
    {
        transactionId = transaction.transactionId;
        cashierName = cashier.fullName();
        studentId = transaction.studentId;
        studentName = student.fullName();
        total = transaction.total;
        discount = transaction.discount;
        dateTime = transaction.dateTime;
        tariffs = transaction.tariffs;
        areas = transaction.areas;
    }

    public string transactionId { get; set; }
    public string cashierName { get; set; }
    public string studentId { get; set; }
    public string studentName { get; set; }
    public float total { get; set; }
    public float discount { get; set; }
    public int areas { get; set; }
    public DateTime dateTime { get; set; }
    public ICollection<TariffEntity> tariffs { get; set; }

}