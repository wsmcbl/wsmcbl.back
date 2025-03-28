using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.management;

public class TeacherReportDto
{
    public string name { get; set; }
    public bool hasSubmittedGrades { get; set; }
    public List<SubjectReportDto> subjectList { get; set; }

    public TeacherReportDto(TeacherEntity teacher)
    {
        name = teacher.fullName();
        hasSubmittedGrades = teacher.hasSubmittedGrades();

        subjectList = [];
        foreach (var item in teacher.subjectGradedList!)
        {
            subjectList.Add(new SubjectReportDto(item));
        }
    }
}