using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public static class DtoMapperSecretary
{
    private static GradeBasicDto mapToBasicDto(this GradeEntity grade) => new(grade);
    private static SchoolYearBasicDto mapToBasicDto(this SchoolYearEntity schoolYear) => new(schoolYear);
    public static GradeDto mapToDto(this GradeEntity grade) => new(grade);
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYear) => new(schoolYear);
    
    
    
    public static List<SchoolYearBasicDto> mapListToDto(this IEnumerable<SchoolYearEntity> list) 
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<input.SubjectDto> mapListToDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new input.SubjectDto(e)).ToList();

    public static List<input.TariffDto> mapListToDto(this IEnumerable<TariffEntity> tariffs)
        => tariffs.Select(e => new input.TariffDto(e)).ToList();

    public static List<input.GradeDto> mapListToDto(this IEnumerable<GradeEntity> grades)
        => grades.Select(e => e.mapToNewSchoolyearDto()).ToList();
    
    private static input.GradeDto mapToNewSchoolyearDto(this GradeEntity grade) => new(grade);
    
    public static List<GradeBasicDto> mapListToBasicDto(this IEnumerable<GradeEntity> grades) 
        => grades.Select(e => e.mapToBasicDto()).ToList();
}