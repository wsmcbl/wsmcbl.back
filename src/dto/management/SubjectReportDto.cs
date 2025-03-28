using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public class SubjectReportDto
{
    public string subjectId { get; set; }
    public int studentCount { get; set; }
    public int gradedStudentCount { get; set; }

    public SubjectReportDto(SubjectGradedView view)
    {
        subjectId = view.subjectId;
        studentCount = view.studentCount;
        gradedStudentCount = view.gradedStudentCount;
    }
}