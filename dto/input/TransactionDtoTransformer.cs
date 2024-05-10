using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.input;

public class TransactionDtoTransformer
{
    public TransactionEntity getTransaction(TransactionDto dto)
    {
        var transaction = new TransactionEntity
        {
            transactionId = "trans1222"+ getRandom(),
            studentId = dto.studentId,
            cashierId = dto.cashierId,
            discount = dto.discount,
            dateTime = dto.dateTime ?? DateTime.Now,
            tariffs = dto.tariffs
        };

        transaction.computeTotal();
        Console.WriteLine(transaction.total);

        return transaction;
    }

    private int getRandom()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        return random.Next(425, 999);
    }
}