using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicSchoolYearDto
{
    [Required] public string schoolYearId { get; set; }
    [Required] public string label { get; set; }
    [Required] public DateOnly startDate { get; set; }
    [Required] public DateOnly deadLine { get; set; }
    [Required] public bool isActive { get; set; }

    public BasicSchoolYearDto()
    {
    }

    public BasicSchoolYearDto(SchoolYearEntity schoolYear)
    {
        schoolYearId = schoolYear.id;
        label = schoolYear.label;
        isActive = schoolYear.isActive;
        startDate = schoolYear.startDate;
        deadLine = schoolYear.deadLine;
    }
}