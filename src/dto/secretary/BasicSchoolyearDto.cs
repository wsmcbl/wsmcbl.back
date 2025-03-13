using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicSchoolyearDto
{
    [Required] public string schoolYearId { get; set; }
    [Required] public string label { get; set; }
    [Required] public DateOnly startDate { get; set; }
    [Required] public DateOnly deadLine { get; set; }
    [Required] public bool isActive { get; set; }

    public BasicSchoolyearDto(SchoolYearEntity schoolyear)
    {
        schoolYearId = schoolyear.id!;
        label = schoolyear.label;
        isActive = schoolyear.isActive;
        startDate = schoolyear.startDate;
        deadLine = schoolyear.deadLine;
    }
}