using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class InvoiceDto
{
    public string transactionId { get; set; } = null!;
    public string cashierName { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public float total { get; set; }
    public DateTime dateTime { get; set; }
    public float[] generalBalance { get; set; } = null!;
    public ICollection<InvoiceDetailDto> detail { get; set; } = null!;

    public InvoiceDto()
    {
    }

    public InvoiceDto(TransactionEntity transaction, StudentEntity student, CashierEntity cashier)
    {
        transactionId = transaction.transactionId!;
        cashierName = cashier.fullName();
        studentId = transaction.studentId;
        studentName = student.fullName();
        total = transaction.total;
        dateTime = transaction.date;
        detail = transaction.details.Select(t => t.mapToDto(student)).ToList();
    }
}