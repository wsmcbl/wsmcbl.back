namespace wsmcbl.src.dto.accounting;

public class ForgetDebtDto
{
    public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    public string authorizationToken { get; set; } = null!;
}