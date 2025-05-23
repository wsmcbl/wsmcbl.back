using wsmcbl.src.model;
using wsmcbl.src.model.secretary;

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
    public static SchoolyearDto mapToDto(this SchoolyearEntity schoolyear) => new(schoolyear);


    private static BasicDegreeToEnrollDto mapToBasicEnrollDto(this DegreeEntity degree) => new(degree);
    private static BasicDegreeDto mapToBasicDto(this DegreeEntity degree) => new(degree);
    private static BasicEnrollmentDto mapToBasicDto(this model.academy.EnrollmentEntity enrollment) => new(enrollment);
    
    public static List<BasicStudentDto> mapToListBasicDto(this List<StudentView> list)
        => list.Select(e => new BasicStudentDto(e)).ToList();

    public static List<BasicDegreeToEnrollDto> mapToListBasicDto(this IEnumerable<DegreeEntity> list)
        => list.Select(e => e.mapToBasicEnrollDto()).ToList();

    public static List<BasicEnrollmentDto> mapToListBasicDto(this IEnumerable<model.academy.EnrollmentEntity> list)
        => list.Select(e => e.mapToBasicDto()).ToList();

    public static List<StudentParentDto> mapListToDto(this IList<StudentParentEntity> list)
        => !list.Any() ? [] : list.Select(item => item.mapToDto()).ToList();

    public static List<BasicSchoolyearDto> mapListToDto(this IEnumerable<SchoolyearEntity> list)
        => list.Select(e => new BasicSchoolyearDto(e)).ToList();

    public static List<TariffDto> mapListToDto(this IEnumerable<model.accounting.TariffEntity> tariffs)
        => tariffs.Select(e => new TariffDto(e)).ToList();

    public static List<DegreeSubjectDto> mapListToDto(this IEnumerable<DegreeEntity> grades)
        => grades.Select(e => new DegreeSubjectDto(e)).ToList();

    public static List<StudentRegisterViewDto> mapListToDto(this List<StudentRegisterView> value)
        => value.Select(e => new StudentRegisterViewDto(e)).ToList();

    public static List<TariffDataDto> mapListToDto(this List<TariffDataEntity> value)
        => value.Select(e => new TariffDataDto(e)).ToList();

    public static List<WithdrawnStudentDto> mapListToDto(this List<model.academy.WithdrawnStudentEntity> value)
        => value.Select(e => new WithdrawnStudentDto(e)).ToList();


    public static List<SubjectDto> mapListToInputDto(this IEnumerable<SubjectEntity> subjects)
        => subjects.Select(e => new SubjectDto(e)).ToList();

    public static List<BasicDegreeDto> mapListToBasicDto(this IEnumerable<DegreeEntity> grades)
        => grades.Select(e => e.mapToBasicDto()).ToList();
}