namespace wsmcbl.src.dto.management;

public class SubjectReportDto
{
    public string subjectId { get; set; }
    public int studentCount { get; set; }
    public int gradedStudentCount { get; set; }

    public SubjectReportDto(string subjectId, int studentCount, int gradedStudentCount)
    {
        this.subjectId = subjectId;
        this.studentCount = studentCount;
        this.gradedStudentCount = gradedStudentCount;
    }
}