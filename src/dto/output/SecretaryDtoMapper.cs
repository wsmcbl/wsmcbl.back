using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public static class SecretaryDtoMapper
{
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYearEntity)
    {
        return new SchoolYearDto();
    }
    
    public static List<SchoolYearDto> mapListToDto(this List<SchoolYearEntity> list)
    {
        return list.Select(e => e.mapToDto()).ToList();
    }
    
    private static GradeBasicDto mapToBasicDto(this GradeEntity grade)
    {
        return new GradeBasicDto
        {
            gradeId = grade.gradeId,
            label = grade.label,
            modality = grade.modality,
            quantity = grade.quantity,
            schoolYear = grade.schoolYear
        };
    }
    
    public static List<GradeBasicDto> mapListToDto(this IEnumerable<GradeEntity> grades)
    {
        return grades.Select(e => e.mapToBasicDto()).ToList();
    }


    public static GradeDto mapToDto(this GradeEntity grade)
    {
        return new GradeDto();
    }
}