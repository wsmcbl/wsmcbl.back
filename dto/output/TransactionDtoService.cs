using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class TransactionDtoService
{
    private TransactionEntity transaction;
    private StudentEntity student;
    private CashierEntity cashier;


    public TransactionDtoService(TransactionEntity transaction, StudentEntity student, CashierEntity cashier)
    {
        this.transaction = transaction;
        this.student = student;
        this.cashier = cashier;
    }

    public TransactionDto getDto()
    {
        return new TransactionDto(transaction, student, cashier);
    }
}