using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public class SchoolYearDto
{
    public string id { get; set; }
    public string label { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    
    public List<input.GradeDto>? grades { get; set; }
    public List<input.TariffDto>? tariffs { get; set; }

    public SchoolYearDto(SchoolYearEntity schoolYear)
    {
        id = schoolYear.id;
        label = schoolYear.label;
        isActive = schoolYear.isActive;
        startDate = schoolYear.startDate;
        deadLine = schoolYear.deadLine;

        grades = schoolYear.gradeList.Count == 0 ? [] : schoolYear.gradeList.mapListToDto();
        tariffs = schoolYear.tariffList.Count == 0 ? [] : schoolYear.tariffList.mapListToDto();
    }
}