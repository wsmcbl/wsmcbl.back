using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class InvoiceDto
{
    public string transactionId { get; set; }
    public string cashierName { get; set; }
    public string studentId { get; set; }
    public string studentName { get; set; }
    public float total { get; set; }
    public DateTime dateTime { get; set; }
    public ICollection<DetailDto> tariffs { get; set; }

}