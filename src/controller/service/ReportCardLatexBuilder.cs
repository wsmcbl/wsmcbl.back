using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    private StudentEntity student = null!;
    private TeacherEntity teacher = null!;
    private List<SemesterEntity> semesterList = null!;
    private List<(string initials, string subjectId)> subjects = null!;
    private string degree = null!;
    private readonly string templatesPath;
    
    public string? unjustifications { get; set; }
    public string? justifications { get; set;}
    public string? lateArrivals { get; set;}

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

    public void setSemesterList(List<SemesterEntity> list)
    {
        semesterList = list;
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
        content = content.Replace($"\\late.arrivals", lateArrivals);
        content = content.Replace($"\\justifications", justifications);
        content = content.Replace($"\\unjustifications", unjustifications);

        return content;
    }
    
    private string getFinalGrade()
    {        
        var finalGrade = "NF";

        var quantity = subjects.Count;
        
        while (quantity < 0)
        {
            finalGrade = $"{finalGrade} & ";
            quantity--;
        }

        return $"{finalGrade}\\\\ \\hline \n";
    }

    private string? getSecondSemester()
    {
        return getSemester(semesterList.First(e => e.semester == 2));
    }

    private string getFirstSemester()
    {
        return getSemester(semesterList.First(e => e.semester == 1));
    }

    private string getSemester(SemesterEntity semester)
    {
        var firstPartial = student.partials.First(e => e.partial == 1 && e.semesterId == semester.semesterId);
        var secondPartial = student.partials.First(e => e.partial == 2 && e.semesterId == semester.semesterId);
        
        var semesterLine = semester.label;

        foreach (var subject in subjects)
        {
            var firstGrade = firstPartial.grades.First(e => e.subjectId == subject.subjectId).grade;
            var secondGrade = secondPartial.grades.First(e => e.subjectId == subject.subjectId).grade;

            var grade = (firstGrade + secondGrade) / 2;
            semesterLine = $"{semesterLine} & {grade.ToString()}";
        }

        semesterLine = $"{semesterLine}\\\\ \\hline \n";
        
        return $"{getPartial(firstPartial)} {getPartial(secondPartial)} {semesterLine}";
    }

    private string getPartial(PartialEntity partial) => getGradeByPartial(partial.grades!, partial.label);

    private string getGradeByPartial(ICollection<GradeEntity> gradeList, string partialLabel)
    {
        var labelLine = partialLabel;
        var gradeLine = " ";

        foreach (var subject in subjects)
        {
            var result = gradeList.FirstOrDefault(e => e.subjectId == subject.subjectId);

            var label = result == null ? "" : result.label;
            var grade = result == null ? "" : result.grade.ToString();

            labelLine = $"{labelLine} & {label}";
            gradeLine = $"{gradeLine} & {grade}";
        }

        var quantity = (subjects.Count + 1).ToString();
        labelLine = $"{labelLine}\\\\ \\cline{{2-{quantity}}}\n";
        gradeLine = $"{gradeLine}\\\\ \\hline \n";

        return $"{labelLine} {gradeLine}";
    }

    private string getColumnQuantity()
    {
        var quantity = (subjects.Count + 1).ToString();
        return $"|*{{{quantity}}}{{c|}}";
    }

    private string getTitleLine()
    {
        var result = "Parcial";

        foreach (var item in subjects)
        {
            result = $"{result} & {item.initials}";
        }

        return $"{result}\\\\";
    }
}