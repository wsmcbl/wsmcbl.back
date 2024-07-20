using wsmcbl.src.model.accounting;
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

        grades = getGrades(schoolYear.gradeList);
        tariffs = getTariffs(schoolYear.tariffList);
    }

    private static List<input.GradeDto> getGrades(List<GradeEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            return [];
        }

        return list.mapListToDto();
    }

    private List<input.TariffDto> getTariffs(List<TariffEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            return [];
        }

        return list.mapListToDto();
    }
}