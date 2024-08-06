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
    private List<(string, string)>? subjects;
    private readonly string grade = "Primero A";

    public void setSubjectList(List<(string, string)> list)
    {
        subjects = list;
    }

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
        content = content.Replace($"\\grade", grade);
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
    
    private string getGradeByPartial(IEnumerable<GradeEntity> grades, string partialLabel)
    {
        var result = partialLabel;

        foreach (var item in grades)
        {
            // Falta asegurar la correspondencia de cada nota con su asignatura
            result = $"{result} && {item.label}";
        }

        var quantity = (subjects.Count + 1).ToString();
        result = $"{result}\\\\ \\cline{{2 - {quantity}}}";
        result = $"{result} ";
        
        foreach (var item in grades)
        {
            result = $"{result} && {item.grade}";
        }

        return result;
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
            result = $"{result} & {item.Item1}";
        }
        
        return $"{result}\\\\ \\hline";
    }
}