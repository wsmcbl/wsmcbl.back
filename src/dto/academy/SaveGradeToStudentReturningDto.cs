namespace wsmcbl.src.dto.academy;

public class SaveGradeToStudentReturningDto
{
    public string studentId { get; set; } = string.Empty;
    public List<GradeItemDto> grades { get; set; } = new();
}

public class GradeItemDto
{
    public int subjectPartialId { get; set; }
    public decimal grade { get; set; }
    public decimal conductgrade { get; set; }
}