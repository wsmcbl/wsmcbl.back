namespace wsmcbl.back.dto.output;

public class TransactionDto
{
    public string? transactionId { get; set; }
    public string cashierId { get; set; } = null!;
    public float total { get; set; }
    public DateTime date { get; set; }
    public ICollection<DetailDto> details { get; set; } = null!;
}