using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.output;

public class SchoolYearBasicDto
{
    [Required] public string schoolYearId { get; set; }
    [Required] public string label { get; set; }
    [Required] public DateOnly startDate { get; set; }
    [Required] public DateOnly deadLine { get; set; }
    [Required] public bool isActive { get; set; }
}