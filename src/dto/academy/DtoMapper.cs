using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public static class DtoMapper
{
    public static List<GradeEntity> toEntity(this IEnumerable<GradeDto> gradeList)
        => gradeList.Select(e => e.toEntity()).ToList();

    private static PartialInformationDto mapToDto(this PartialEntity partial) => new(partial);

    private static SubjectPartialDto mapToDto(this SubjectPartialEntity subjectPartial) => new(subjectPartial);

    private static GradeDto mapToDto(this GradeEntity grade) => new(grade);
    
    public static TeacherDto mapToDto(this TeacherEntity value) => new(value);

    public static EnrollmentGuideDto mapToDto(this EnrollmentEntity? value)
        => new(value);


    private static BasicTeacherDto mapToBasicDto(this TeacherEntity teacher) => new(teacher);

    private static BasicStudentDto mapToBasicDto(this StudentEntity student) => new(student);

    private static BasicSubjectDto mapToBasicDto(this model.secretary.SubjectEntity subject) => new(subject);


    public static List<BasicTeacherDto> mapListToDto(this IEnumerable<TeacherEntity> teacherList) =>
        teacherList.Select(e => e.mapToBasicDto()).ToList();

    public static List<BasicStudentDto> mapListToDto(this IEnumerable<StudentEntity> studentList) =>
        studentList.Select(e => e.mapToBasicDto()).ToList();

    public static List<EnrollmentByTeacherDto> mapListToDto(this IEnumerable<EnrollmentEntity> enrollmentList,
        string teacherId) =>
        enrollmentList.Select(e => new EnrollmentByTeacherDto(e, teacherId)).ToList();

    public static List<PartialInformationDto> mapListToDto(this IEnumerable<PartialEntity> partialList) =>
        partialList.Select(e => e.mapToDto()).ToList();

    public static List<SubjectPartialDto> mapListToDto(this IEnumerable<SubjectPartialEntity> subjectPartialList) =>
        subjectPartialList.Select(e => e.mapToDto()).ToList();

    public static List<GradeDto> mapListToDto(this IEnumerable<GradeEntity> gradeList) =>
        gradeList.Select(e => e.mapToDto()).ToList();

    public static List<SubjectDto> mapListToDto(this IEnumerable<model.secretary.SubjectEntity> list) =>
        list.Select(e => new SubjectDto(e)).ToList();
    
    public static List<StudentAverageDto> mapListToDto(this List<StudentEntity> list) =>
        list.Select(e => new StudentAverageDto(e)).ToList();


    public static List<BasicSubjectDto> mapListToBasicDto(this IEnumerable<SubjectEntity> list)
    {
        var result = list.Select(e => e.secretarySubject).ToList();
        return result.Select(e => e!.mapToBasicDto()).ToList();
    }
}