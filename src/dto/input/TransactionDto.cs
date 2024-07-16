using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.input;

public class TransactionDto
{
    [Required]
    public string cashierId { get; set; } = null!;

    [Required]
    public string studentId { get; set; } = null!;
    
    [JsonRequired]
    public DateTime dateTime { get; set; }
    
    [Required]
    public ICollection<DetailDto> details { get; set; } = new List<DetailDto>();

    public virtual List<DebtHistoryEntity> getDetail()
    {
        return details.toEntity();
    }

    public virtual TransactionEntity toEntity()
    {
        var transaction = new TransactionEntity
        {
            studentId = studentId,
            cashierId = cashierId,
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