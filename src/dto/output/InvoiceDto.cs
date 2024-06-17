namespace wsmcbl.src.dto.output;

public class InvoiceDto
{
    public string transactionId { get; set; } = null!;
    public string cashierName { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public float total { get; set; }
    public DateTime dateTime { get; set; }
    public float[] generalBalance { get; set; } = null!;
    public ICollection<DetailDto> detail { get; set; } = null!;
}