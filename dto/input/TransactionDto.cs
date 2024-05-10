using System.ComponentModel.DataAnnotations;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.input;

public class TransactionDto
{
    [Required]
    public string? cashierId { get; set; }
    
    [Required]
    public string? studentId { get; set; }
    
    public float discount { get; set; }
    
    [Required]
    public DateTime? dateTime { get; set; }
    
    [Required]
    public ICollection<TariffEntity>? tariffs { get; set; } = new List<TariffEntity>();
}