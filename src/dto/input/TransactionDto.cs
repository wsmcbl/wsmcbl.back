using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
    public ICollection<DetailDto>? details { get; set; } = new List<DetailDto>();
}