using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.accounting;

public class ChangeStudentDiscountDto
{
    [Required] public string studentId { get; set; } = null!;
    [Required] public int discountId { get; set; }
    [Required] public string authorizationToken { get; set; } = null!;
}