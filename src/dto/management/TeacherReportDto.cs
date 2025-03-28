namespace wsmcbl.src.dto.management;

public class TeacherReportDto
{
    public string name { get; set; }
    public bool hasSubmittedGrades { get; set; }
    public List<SubjectReportDto> subjectList { get; set; }

    public TeacherReportDto(string Name, bool HasSubmittedGrades, List<object> list)
    {
        name = Name;
        hasSubmittedGrades = HasSubmittedGrades;
        subjectList = new List<SubjectReportDto>();
    }
}