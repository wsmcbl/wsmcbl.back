using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class SchoolYearDto
{
    public string id { get; set; }
    public string label { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public bool isActive { get; set; }
    
    public List<DegreeToCreateDto>? degreeList { get; set; }
    public List<TariffToCreateDto>? tariffList { get; set; }

    public SchoolYearDto(SchoolYearEntity schoolYear)
    {
        id = schoolYear.id!;
        label = schoolYear.label;
        isActive = schoolYear.isActive;
        startDate = schoolYear.startDate;
        deadLine = schoolYear.deadLine;

        degreeList = getGrades(schoolYear.degreeList);
        tariffList = getTariffs(schoolYear.tariffList);
    }

    private static List<DegreeToCreateDto> getGrades(List<DegreeEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            return [];
        }

        return list.mapListToDto();
    }

    private static List<TariffToCreateDto> getTariffs(List<TariffEntity>? list)
    {
        if (list == null || list.Count == 0)
        {
            return [];
        }

        return list.mapListToDto();
    }
}