namespace wsmcbl.src.model;

public class TransactionReportViewPagedRequest : PagedRequest
{
    public string? to { get; set; }
    public string? from { get; set; }
}