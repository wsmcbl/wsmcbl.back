namespace wsmcbl.src.dto.academy;

public class SubjectPartialDto
{
    public int partialId { get; set; }
    public string subjectId { get; set; } = null!;
    public List<GradesToAddDto> grades { get; set; } = null!;

    public SubjectPartialDto()
    {
    }
}