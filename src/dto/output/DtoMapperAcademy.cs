using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.output;

public static class DtoMapperAcademy
{
    private static TeacherBasicDto mapToBasicDto(this TeacherEntity teacher)
    {
        return new TeacherBasicDto
        {
            teacherId = teacher.teacherId,
            fullName = teacher.fullName(),
            isGuide = teacher.isGuide
        };
    }
    
    public static List<TeacherBasicDto> mapListToDto(this IEnumerable<TeacherEntity> teacherList)
    {
        return teacherList.Select(e => e.mapToBasicDto()).ToList();
    }

}