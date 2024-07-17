using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.output;

public static class AcademyDtoMapper
{
    private static TeacherBasicDto mapToBasicDto(this TeacherEntity teacher)
    {
        return new TeacherBasicDto();
    }
    
    public static List<TeacherBasicDto> mapListToDto(this IEnumerable<TeacherEntity> teacherList)
    {
        return teacherList.Select(e => e.mapToBasicDto()).ToList();
    }

}