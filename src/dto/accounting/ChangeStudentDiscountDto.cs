using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace wsmcbl.src.dto.accounting;

public class ChangeStudentDiscountDto
{
    [Required] public string studentId { get; set; } = null!;
    [JsonRequired] public int discountId { get; set; }
    [Required] public string authorizationToken { get; set; } = null!;
}