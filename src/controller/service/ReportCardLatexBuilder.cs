using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    private readonly string templatesPath;
    public ReportCardLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
    }

    protected override string getTemplateName() => "report-card";

    private StudentEntity? student;
    private TeacherEntity? teacher;
    public void setProperties(StudentEntity studentParameter, TeacherEntity teacherParameter)
    {
        student = studentParameter;
        teacher = teacherParameter;
    }

    protected override string updateContent(string content)
    {
        if (student == null || teacher == null)
        {
            return content;
        }
        
        content = content.Replace($"\\cbl-logo-wb", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.Replace($"\\date", DateTime.Today.Date.ToString("dd/MM/yyyy"));
        content = content.Replace($"\\student.name", student.fullName());
        content = content.Replace($"\\teacher.name", teacher.fullName());
        content = content.Replace($"\\grade", "hola");
        content = content.Replace($"\\titleLine", "a & b\\\\");
        content = content.Replace($"\\firstSemester", "r & b\\\\");
        content = content.Replace($"\\secondSemester", "b & b\\\\");
        content = content.Replace($"\\finalGrade", "a & a\\\\");
        content = content.Replace($"\\footLine", "a & w\\\\");
        
        return content;
    }
}