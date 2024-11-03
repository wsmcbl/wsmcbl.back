using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class SubjectPartialDto
{
    public int partialId { get; set; }
    public string subjectId { get; set; }
    public List<GradesToAddDto> grades { get; set; }

    public SubjectPartialDto(SubjectPartialEntity subjectPartial)
    {
        partialId = subjectPartial.partialId;
        subjectId = subjectPartial.subjectId;
        grades = subjectPartial.gradeList.mapListToDto();
    }
}