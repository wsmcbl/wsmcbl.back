using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class GradeListDto
{
    public List<GradeDto> gradeList { get; set; } = null!;

    public List<GradeEntity> getList()
    {
        return gradeList.toEntity();
    }
}