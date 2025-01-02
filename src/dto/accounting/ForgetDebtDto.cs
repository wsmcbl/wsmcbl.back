using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.accounting;

public class ForgetDebtDto
{
    [Required] public string studentId { get; set; } = null!;
    public int tariffId { get; set; }
    [Required] public string authorizationToken { get; set; } = null!;
}