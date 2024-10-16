namespace wsmcbl.src.dto.academy;

public class SubjectPartialDto
{
    public string subjectId { get; set; } = null!;
    public List<GradesToAddDto> grades { get; set; } = null!;
}