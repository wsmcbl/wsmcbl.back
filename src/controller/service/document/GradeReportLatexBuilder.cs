using System.Text;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;
using SubjectEntity = wsmcbl.src.model.academy.SubjectEntity;

namespace wsmcbl.src.controller.service.document;

public class GradeReportLatexBuilder : LatexBuilder
{
    public GradeReportLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
    }
    
    private StudentEntity student { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private List<SemesterEntity> semesterList { get; set; } = null!;
    private List<SubjectEntity> subjectList { get; set; } = null!;
    private List<SubjectAreaEntity> subjectAreaList { get; set; } = null!;
    private string principalName { get; set; } = null!;
    
    private DegreeEntity degree { get; set; } = null!;
    private string userName { get; set; } = null!;

    protected override string getTemplateName() => "grade-report";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder();
        content.ReplaceInLatexFormat("logo.value", $"{getImagesPath()}/cbl-logo-wb.png");
        
        content.Replace("schoolyear.value", DateTime.Today.Year.ToString());
        content.Replace("degree.value", degree.label);
        
        content.Replace("mined.id.value", student.student.minedId ?? "N/A");
        content.Replace("student.name.value", student.fullName());
        content.Replace("teacher.name.value", teacher.fullName());
        content.Replace("principal.name.value", principalName);
        content.Replace("educational.level.value", degree.educationalLevel);
        
        content.Replace("shift.value", "Matutino");
        content.Replace("department.value", "Managua");
        content.Replace("municipality.value", "Managua");
        content.Replace("school.id.value", "14484");
        
        content.Replace("detail.value", string.Empty);
        
        content.ReplaceInLatexFormat("secretary.name.value", userName);
        content.ReplaceInLatexFormat("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));

        return content.ToString();
    }
}