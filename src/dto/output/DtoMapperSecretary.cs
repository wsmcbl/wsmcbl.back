using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.dto.output;

public static class DtoMapperSecretary
{

    public static StudentFullDto mapToDto(this StudentEntity student) => new StudentFullDto();
    public static GradeDto mapToDto(this GradeEntity grade) => new(grade);
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYear) => new(schoolYear);



    private static GradeBasicToEnrollDto mapToBasicEnrollDto(this GradeEntity grade) => new GradeBasicToEnrollDto();
    private static StudentBasicToEnrollDto mapToBasicDto(this StudentEntity student)
        => new StudentBasicToEnrollDto();
    private static GradeBasicDto mapToBasicDto(this GradeEntity grade) => new(grade);
    private static SchoolYearBasicDto mapToBasicDto(this SchoolYearEntity schoolYear)
        => SchoolYearBasicDto.init(schoolYear);



    public static List<GradeBasicToEnrollDto> mapToListBasicDto(this IEnumerable<GradeEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();
    
    public static List<StudentBasicToEnrollDto> mapToListBasicDto(this IEnumerable<StudentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<SchoolYearBasicDto> mapListToDto(this IEnumerable<SchoolYearEntity> list) 
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<input.SubjectDto> mapListToDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new input.SubjectDto.Builder(e).build()).ToList();

    public static List<input.TariffDto> mapListToDto(this IEnumerable<TariffEntity> tariffs)
        => tariffs.Select(e => new input.TariffDto.Builder(e).build()).ToList();

    public static List<input.GradeDto> mapListToDto(this IEnumerable<GradeEntity> grades)
        => grades.Select(e => e.mapToNewSchoolyearDto()).ToList();
    
    private static input.GradeDto mapToNewSchoolyearDto(this GradeEntity grade)
        => new input.GradeDto.Builder(grade).build();
    
    public static List<GradeBasicDto> mapListToBasicDto(this IEnumerable<GradeEntity> grades) 
        => grades.Select(e => e.mapToBasicDto()).ToList();
}