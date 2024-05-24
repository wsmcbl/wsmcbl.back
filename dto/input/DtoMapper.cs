using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.input;

public static class DtoMapper
{
    private static TransactionTariffEntity toEntity(this DetailDto dto, string transactionId)
    {
        return new TransactionTariffEntity
        {
            transactionId = transactionId,
            tariffId = dto.tariffId,
            discount = dto.discount,
            arrears = dto.arrears,
            subTotal = 0
        }; 
    }

    public static TransactionEntity toEntity(this TransactionDto dto)
    {
        var transaction = new TransactionEntity
        {
            transactionId = "trans1222" + getRandom(),
            studentId = dto.studentId,
            cashierId = dto.cashierId,
            date = dto.dateTime,
            total = 0
        };

        foreach (var item in dto.details!)
        {
            transaction.details.Add(item.toEntity(transaction.transactionId));
        }
        
        return transaction;
    }
    
    private static int getRandom()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        return random.Next(425, 999);
    }
}