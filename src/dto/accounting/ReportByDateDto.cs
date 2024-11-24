using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class ReportByDateDto
{
    public string userName { get; set; } = null!;
    public DateTime consultedIn { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public int validQuantity { get; set; }
    public double validTotal { get; set; }
    public int invalidQuantity { get; set; }
    public double invalidTotal { get; set; }

    public List<TransactionReportDto> transactionList { get; set; }

    public ReportByDateDto(IEnumerable<(TransactionEntity transaction, StudentEntity student)> list)
    {
        transactionList = list.mapToListDto();
    }
}