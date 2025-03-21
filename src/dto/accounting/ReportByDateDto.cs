using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.accounting;

public class ReportByDateDto
{
    public string userName { get; set; } = string.Empty;
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public int validQuantity { get; set; }
    public decimal validTotal { get; set; }
    public int invalidQuantity { get; set; }
    public decimal invalidTotal { get; set; }
    public DateTime consultedIn { get; set; } = DateTime.UtcNow.toUTC6();
}