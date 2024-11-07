using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TransactionDto
{
    public string transactionId { get; set; }
    [Required] public string? cashierId { get; set; }
    [Required] public string? studentId { get; set; }
    [JsonRequired] public DateTime dateTime { get; set; }
    [Required] public List<TransactionDetailDto> details { get; set; } = [];
    
    public virtual List<DebtHistoryEntity> getDetailToApplyArrears() => details.toEntity();


    public TransactionDto()
    {
        transactionId = string.Empty;
    }

    public TransactionDto(TransactionEntity entity)
    {
        transactionId = entity.transactionId!;
        cashierId = entity.cashierId;
        studentId = entity.studentId;
        dateTime = entity.date;
    }
    
    public virtual TransactionEntity toEntity()
    {
        var transaction = new TransactionEntity
        {
            studentId = studentId!,
            cashierId = cashierId!,
            date = dateTime,
            total = 0
        };

        foreach (var item in details)
        {
            transaction.details.Add(item.toEntity());
        }

        return transaction;
    }
}