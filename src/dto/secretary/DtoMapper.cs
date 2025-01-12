using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.dto.secretary;

public static class DtoMapper
{
    public static List<StudentParentEntity> toEntityList(this IList<StudentParentDto>? list)
        => list == null || !list.Any() ? [] : list.Select(item => item.toEntity()).ToList();
    
    public static EnrollStudentDto mapToDto(this StudentEntity student, (string? enrollmentId, int discountId, bool isRepeating) ids)
        => new(student, ids);
    public static StudentFullDto mapToDto(this StudentEntity value) => new(value);
    public static StudentFileDto mapToDto(this StudentFileEntity? file) => new(file);
    public static StudentTutorDto mapToDto(this StudentTutorEntity tutor) => new(tutor);
    private static StudentParentDto mapToDto(this StudentParentEntity parent) => new(parent);
    public static StudentMeasurementsDto mapToDto(this StudentMeasurementsEntity? measurements) => new(measurements);
    public static SchoolYearDto mapToDto(this SchoolYearEntity schoolYear) => new(schoolYear);


    private static BasicDegreeToEnrollDto mapToBasicEnrollDto(this DegreeEntity degree) => new(degree);
    private static BasicStudentToEnrollDto mapToBasicEnrollDto(this StudentEntity student) => new(student);
    private static BasicStudentDto mapToBasicDto(this StudentEntity entity, string schoolyear, string enrollment) => new(entity, schoolyear, enrollment);
    private static BasicDegreeDto mapToBasicDto(this DegreeEntity degree) => new(degree);
    private static BasicSchoolYearDto mapToBasicDto(this SchoolYearEntity schoolYear) => new(schoolYear);
    private static BasicEnrollmentDto mapToBasicDto(this EnrollmentEntity enrollment) => new(enrollment);


    public static List<BasicDegreeToEnrollDto> mapToListBasicDto(this IEnumerable<DegreeEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();

    public static List<BasicEnrollmentDto> mapToListBasicDto(this IEnumerable<EnrollmentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();

    public static List<BasicStudentDto> mapToListBasicDto(this IEnumerable<(StudentEntity student, string schoolyear, string enrollment)> list)
        => list.Select(e => e.student.mapToBasicDto(e.schoolyear, e.enrollment)).ToList();

    public static List<BasicStudentToEnrollDto> mapToListBasicEnrollDto(this IEnumerable<StudentEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();

    public static List<StudentParentDto> mapListToDto(this IList<StudentParentEntity> list)
        => !list.Any() ? [] : list.Select(item => item.mapToDto()).ToList();

    public static List<BasicSchoolYearDto> mapListToDto(this IEnumerable<SchoolYearEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();

    public static List<TariffToCreateDto> mapListToDto(this IEnumerable<TariffEntity> tariffs)
        => tariffs.Select(e => new TariffToCreateDto(e)).ToList();

    public static List<DegreeToCreateDto> mapListToDto(this IEnumerable<DegreeEntity> grades)
        => grades.Select(e => e.mapToNewSchoolyearDto()).ToList();


    public static List<SubjectToCreateDto> mapListToInputDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new SubjectToCreateDto(e)).ToList();

    private static DegreeToCreateDto mapToNewSchoolyearDto(this DegreeEntity degree) => new(degree);

    public static List<BasicDegreeDto> mapListToBasicDto(this IEnumerable<DegreeEntity> grades)
        => grades.Select(e => e.mapToBasicDto()).ToList();
}