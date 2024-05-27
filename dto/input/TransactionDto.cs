using System.ComponentModel.DataAnnotations;

namespace wsmcbl.back.dto.input;

public class TransactionDto
{
    [Required]
    public string cashierId { get; set; } = null!;

    [Required]
    public string studentId { get; set; } = null!;
    
    [Required]
    public DateTime dateTime { get; set; }
    
    [Required]
    public ICollection<DetailDto>? details { get; set; } = new List<DetailDto>();
}