using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

/*
 * toEntity()
 * mapToDto()
 * mapListToDto()
 * mapListToBasicDto()
 */

public static class DtoMapper
{
    public static List<GradeEntity> toEntity(this IEnumerable<GradeDto> gradeList)
        => gradeList.Select(e => e.toEntity()).ToList();
    
    
    
    private static EnrollmentByTeacherDto mapToDto(this EnrollmentEntity enrollment) => new(enrollment);
    
    private static PartialInformationDto mapToDto(this PartialEntity partial) => new(partial);
    
    private static SubjectPartialDto mapToDto(this SubjectPartialEntity subjectPartial) => new(subjectPartial);

    private static GradeDto mapToDto(this GradeEntity grade) => new(grade);
    
    
    
    private static BasicTeacherDto mapToBasicDto(this TeacherEntity teacher) => new(teacher);
    
    private static BasicStudentDto mapToBasicDto(this StudentEntity student) => new(student);
    
    private static BasicSubjectDto mapToBasicDto(this SubjectEntity subject) => new (subject);
    
    
    
    public static List<BasicTeacherDto> mapListToDto(this IEnumerable<TeacherEntity> teacherList) => 
        teacherList.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<BasicStudentDto> mapListToDto(this IEnumerable<StudentEntity> studentList) => 
        studentList.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<EnrollmentByTeacherDto> mapListToDto(this IEnumerable<EnrollmentEntity> enrollmentList) =>
        enrollmentList.Select(e => e.mapToDto()).ToList();
    
    public static List<PartialInformationDto> mapListToDto(this IEnumerable<PartialEntity> partialList) =>
        partialList.Select(e => e.mapToDto()).ToList();
    
    public static List<SubjectPartialDto> mapListToDto(this IEnumerable<SubjectPartialEntity> subjectPartialList) => 
        subjectPartialList.Select(e => e.mapToDto()).ToList();

    public static List<GradeDto> mapListToDto(this IEnumerable<GradeEntity> gradeList) =>
        gradeList.Select(e => e.mapToDto()).ToList();
    
    
    
    public static List<BasicSubjectDto> mapListToBasicDto(this IEnumerable<SubjectEntity> studentList) => 
        studentList.Select(e => e.mapToBasicDto()).ToList();
}