using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicSchoolyearDto
{
    public string schoolyearId { get; set; }
    public string label { get; set; }
    public DateOnlyDto startDate { get; set; }
    public DateOnlyDto deadLine { get; set; }
    public bool isActive { get; set; }

    public BasicSchoolyearDto(SchoolyearEntity schoolyear)
    {
        schoolyearId = schoolyear.id!;
        label = schoolyear.label;
        isActive = schoolyear.isActive;
        startDate = new DateOnlyDto(schoolyear.startDate);
        deadLine = new DateOnlyDto(schoolyear.deadLine);
    }
}