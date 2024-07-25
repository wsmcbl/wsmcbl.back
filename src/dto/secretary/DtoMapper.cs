using wsmcbl.src.dto.output;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.dto.secretary;

public static class DtoMapper
{
    public static List<StudentParentEntity> toEntity(this IEnumerable<StudentParentDto> list)
    {
        return list.Select(item => item.toEntity()).ToList();
    }
    
    public static StudentFullDto mapToDto(this StudentEntity student) => new(student);
    public static GradeDto mapToDto(this GradeEntity grade) => new(grade);
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYear) => new(schoolYear);



    private static GradeBasicToEnrollDto mapToBasicEnrollDto(this GradeEntity grade) => new(grade);
    private static StudentBasicToEnrollDto mapToBasicDto(this StudentEntity student) => new(student);
    private static GradeBasicDto mapToBasicDto(this GradeEntity grade) => new(grade);
    private static SchoolYearBasicDto mapToBasicDto(this SchoolYearEntity schoolYear) => new(schoolYear);
    private static EnrollmentBasicDto mapToBasicDto(this EnrollmentEntity enrollment) => new(enrollment);



    public static List<GradeBasicToEnrollDto> mapToListBasicDto(this IEnumerable<GradeEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();
    public static List<EnrollmentBasicDto> mapToListBasicDto(this IEnumerable<EnrollmentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<StudentBasicToEnrollDto> mapToListBasicDto(this IEnumerable<StudentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<SchoolYearBasicDto> mapListToDto(this IEnumerable<SchoolYearEntity> list) 
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<SubjectDto> mapListToDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new SubjectDto.Builder(e).build()).ToList();

    public static List<TariffDto> mapListToDto(this IEnumerable<TariffEntity> tariffs)
        => tariffs.Select(e => new TariffDto.Builder(e).build()).ToList();

    public static List<GradeDto> mapListToDto(this IEnumerable<GradeEntity> grades)
        => grades.Select(e => e.mapToNewSchoolyearDto()).ToList();
    
    private static GradeDto mapToNewSchoolyearDto(this GradeEntity grade)
        => new GradeDto(grade);
    
    public static List<GradeBasicDto> mapListToBasicDto(this IEnumerable<GradeEntity> grades) 
        => grades.Select(e => e.mapToBasicDto()).ToList();
}