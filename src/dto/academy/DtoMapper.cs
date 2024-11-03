using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public static class DtoMapper
{
    private static GradeEntity toEntity(this SubjectPartialDto dto)
    {
        return new GradeEntity();
    }
    
    private static EnrollmentByTeacherDto mapToDto(this EnrollmentEntity enrollment) => new(enrollment);
    private static PartialInformationDto mapToDto(this PartialEntity partial) => new(partial);
    
    private static BasicTeacherDto mapToBasicDto(this TeacherEntity teacher) => new(teacher);
    
    private static BasicStudentDto mapToBasicDto(this StudentEntity student) => new(student);
    
    public static List<BasicTeacherDto> mapListToDto(this IEnumerable<TeacherEntity> teacherList) => 
        teacherList.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<BasicStudentDto> mapListToDto(this IEnumerable<StudentEntity> studentList) => 
        studentList.Select(e => e.mapToBasicDto()).ToList();
    
    public static List<EnrollmentByTeacherDto> mapListToDto(this IEnumerable<EnrollmentEntity> enrollmentList) =>
        enrollmentList.Select(e => e.mapToDto()).ToList();
    
    public static List<PartialInformationDto> mapListToDto(this IEnumerable<PartialEntity> partialList) =>
        partialList.Select(e => e.mapToDto()).ToList();
    
    public static List<GradeEntity> toEntity(this IEnumerable<SubjectPartialDto> list)
        => list.Select(e => e.toEntity()).ToList();
}