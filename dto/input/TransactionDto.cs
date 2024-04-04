using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.input;

public class TransactionDto
{
    public string cashierId { get; set; }
    public string studentId { get; set; }
    public float discount { get; set; }
    public DateTime dateTime { get; set; }
    public ICollection<TariffEntity> tariffs { get; set; } = new List<TariffEntity>();
}