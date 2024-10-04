using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class TransactionDto
{
    [Required] public string? cashierId { get; set; }
    [Required] public string? studentId { get; set; }
    [JsonRequired] public DateTime dateTime { get; set; }
    [Required] public List<DetailDto> details { get; set; } = [];
    
    public virtual List<DebtHistoryEntity> getDetailToApplyArrears() => details.toEntity();

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