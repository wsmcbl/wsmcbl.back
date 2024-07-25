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
    public static GradeToCreateDto mapToDto(this GradeEntity grade) => new(grade);
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYear) => new(schoolYear);



    private static BasicGradeToEnrollDto mapToBasicEnrollDto(this GradeEntity grade) => new(grade);
    private static BasicStudentToEnrollDto mapToBasicDto(this StudentEntity student) => new(student);
    private static BasicGradeDto mapToBasicDto(this GradeEntity grade) => new(grade);
    private static BasicSchoolYearDto mapToBasicDto(this SchoolYearEntity schoolYear) => new(schoolYear);
    private static BasicEnrollmentDto mapToBasicDto(this EnrollmentEntity enrollment) => new(enrollment);



    public static List<BasicGradeToEnrollDto> mapToListBasicDto(this IEnumerable<GradeEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();
    public static List<BasicEnrollmentDto> mapToListBasicDto(this IEnumerable<EnrollmentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<BasicStudentToEnrollDto> mapToListBasicDto(this IEnumerable<StudentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<BasicSchoolYearDto> mapListToDto(this IEnumerable<SchoolYearEntity> list) 
        => list.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<SubjectDto> mapListToDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new SubjectDto(e)).ToList();

    public static List<TariffToCreateDto> mapListToDto(this IEnumerable<TariffEntity> tariffs)
        => tariffs.Select(e => new TariffToCreateDto(e)).ToList();

    public static List<GradeToCreateDto> mapListToDto(this IEnumerable<GradeEntity> grades)
        => grades.Select(e => e.mapToNewSchoolyearDto()).ToList();
    
    private static GradeToCreateDto mapToNewSchoolyearDto(this GradeEntity grade)
        => new GradeToCreateDto(grade);
    
    public static List<BasicGradeDto> mapListToBasicDto(this IEnumerable<GradeEntity> grades) 
        => grades.Select(e => e.mapToBasicDto()).ToList();
}