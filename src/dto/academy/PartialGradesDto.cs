namespace wsmcbl.src.dto.academy;

public class PartialGradesDto
{
    public string teacherId { get; set; } = null!;
    public string partialId { get; set; } = null!;
    public List<SubjectPartialDto> subjectsGrades { get; set; } = null!;
}