using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicSchoolyearDto
{
    public string schoolyearId { get; set; }
    public string label { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }

    public BasicSchoolyearDto(SchoolYearEntity schoolyear)
    {
        schoolyearId = schoolyear.id!;
        label = schoolyear.label;
        isActive = schoolyear.isActive;
        startDate = schoolyear.startDate;
        deadLine = schoolyear.deadLine;
    }
}