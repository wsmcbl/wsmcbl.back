using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    public ReportCardLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
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
        
        content = content.Replace($"\\date", DateTime.Today.Date.ToString("dd/MM/yyyy"));
        content = content.Replace($"\\student.name", student.fullName());
        content = content.Replace($"\\teacher.name", teacher.fullName());
        
        return content;
    }
}