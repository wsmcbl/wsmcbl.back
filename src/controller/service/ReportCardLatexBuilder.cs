using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    private StudentEntity student = null!;
    private TeacherEntity teacher = null!;
    private List<(string initials, string subjectId)> subjects = null!;
    private string degree = null!;
    private readonly string templatesPath;

    public ReportCardLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
    }

    public void setStudent(StudentEntity student)
    {
        this.student = student;
    }

    public void setTeacher(TeacherEntity teacher)
    {
        this.teacher = teacher;
    }

    public void setSubjectsSort(List<(string, string)> list)
    {
        subjects = list;
    }

    public void setDegree(string degree)
    {
        this.degree = degree;
    }

    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.Replace($"\\cbl-logo-wb", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.Replace($"\\date", DateTime.Today.Date.ToString("dd/MM/yyyy"));
        content = content.Replace($"\\student.name", student.fullName());
        content = content.Replace($"\\teacher.name", teacher.fullName());
        content = content.Replace($"\\grade", degree);
        content = content.Replace($"\\column.quantity", getColumnQuantity());
        content = content.Replace($"\\titleLine", getTitleLine());
        content = content.Replace($"\\firstSemester", getFirstSemester());
        content = content.Replace($"\\secondSemester", getSecondSemester());
        content = content.Replace($"\\finalGrade", getFinalGrade());
        content = content.Replace($"\\footLine", getFootLine());

        return content;
    }

    private string? getFootLine()
    {
        return getTitleLine();
    }

    private string? getFinalGrade()
    {
        return getFirstSemester();
    }

    private string? getSecondSemester()
    {
        return getFirstSemester();
    }

    private string getFirstSemester()
    {
        return $"{getFirstPartial()}\\hline {getSecondPartial()} \\hline";
    }
    
    private string getFirstPartial()
    {
        var partial = student.partials.First(e => e.partial == 1);

        return getGradeByPartial(partial.grades!, "I Parcial");
    }
    
    private string getSecondPartial()
    {
        var partial = student.partials.First(e => e.partial == 2);
        
        return getGradeByPartial(partial.grades!, "II Parcial");
    }
    
    private string getGradeByPartial(ICollection<GradeEntity> gradeList, string partialLabel)
    {
        var labelLine = partialLabel;
        var gradeLine = " ";

        foreach (var subject in subjects)
        {
            var grade = gradeList.First(e => e.subjectId == subject.subjectId);
            labelLine = $"{labelLine} & {grade.label}";
            gradeLine = $"{gradeLine} & {grade.grade}";
        }

        var quantity = (subjects.Count + 1).ToString();
        labelLine = $"{labelLine}\\\\ \\cline{{2 - {quantity}}}";
        
        return $"{labelLine} {gradeLine}";
    }

    private string getColumnQuantity()
    {
        var quantity = (subjects.Count + 1).ToString();
        return $"*{{{quantity}}}{{c}}";
    }

    private string getTitleLine()
    {
        var result = "Parcial";

        foreach (var item in subjects)
        {
            result = $"{result} & {item.initials}";
        }
        
        return $"{result}\\\\ \\hline";
    }
}