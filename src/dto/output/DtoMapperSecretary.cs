using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public static class DtoMapperSecretary
{
    private static SchoolYearBasicDto mapToBasicDto(this SchoolYearEntity schoolYear)
    {
        return new SchoolYearBasicDto
        {
            schoolYearId = schoolYear.id,
            label = schoolYear.label,
            isActive = schoolYear.isActive,
            startDate = schoolYear.startDate,
            deadLine = schoolYear.deadLine
        };
    }
    
    private static GradeBasicDto mapToBasicDto(this GradeEntity grade)
    {
        return new GradeBasicDto
        {
            gradeId = grade.gradeId!,
            label = grade.label,
            modality = grade.modality,
            quantity = grade.quantity,
            schoolYear = grade.schoolYear
        };
    }
    
    
    
    
    public static List<SchoolYearBasicDto> mapListToDto(this IEnumerable<SchoolYearEntity> list)
    {
        return list.Select(e => e.mapToBasicDto()).ToList();
    }
    
    public static List<GradeBasicDto> mapListToDto(this IEnumerable<GradeEntity> grades)
    {
        return grades.Select(e => e.mapToBasicDto()).ToList();
    }
}