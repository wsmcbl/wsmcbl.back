using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student = null!;
    private TeacherEntity teacher = null!;
    private List<SemesterEntity> _semesters = null!;
    private List<(string initials, string subjectId)> subjects = null!;
    
    private string degree = null!; 
    private string lateArrivals = null!; 
    private string justifications = null!;
    private string unjustifications = null!;
    
    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.Replace($"\\cbl-logo-wb", $"{getImagesPath()}/cbl-logo-wb.png");
        content = content.Replace($"\\year", DateTime.Today.Year.ToString());
        content = content.Replace($"\\student.name", student.fullName());
        content = content.Replace($"\\teacher.name", teacher.fullName());
        content = content.Replace($"\\degree", degree);
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

    private string getFirstSemester()
    {
        return getSemester(_semesters.First(e => e.semester == 1));
    }

    private string getSecondSemester()
    {
        return getSemester(_semesters.First(e => e.semester == 2));
    }
    
    private string getFinalGrade()
    {        
        var finalGrade = "NF";

        var quantity = subjects.Count;
        
        while (quantity > 0)
        {
            finalGrade = $"{finalGrade} & ";
            quantity--;
        }

        return $"{finalGrade}\\\\ \\hline \n";
    }
    

    
    private string getSemester(SemesterEntity semester)
    {
        var firstPartial = student.partials.First(e => e.partial == 1 && e.semesterId == semester.semesterId);
        var secondPartial = student.partials.First(e => e.partial == 2 && e.semesterId == semester.semesterId);
        
        var semesterLine = semester.label;

        foreach (var subject in subjects)
        {
            var firstGrade = 80;//firstPartial.grades.First(e => e.subjectId == subject.subjectId).grade;
            var secondGrade = 70;//secondPartial.grades.First(e => e.subjectId == subject.subjectId).grade;

            var grade = (firstGrade + secondGrade) / 2;
            semesterLine = $"{semesterLine} & {grade.ToString()}";
        }

        semesterLine = $"{semesterLine}\\\\ \\hline \n";
        
        return $"{getGradeByPartial(firstPartial)} {getGradeByPartial(secondPartial)} {semesterLine}";
    }

    private string getGradeByPartial(PartialEntity partial)
    {
        var labelLine = partial.label;
        var gradeLine = " ";

        foreach (var subject in subjects)
        {
            var result = partial.grades.FirstOrDefault(e => e.subjectId == subject.subjectId);

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
    
    

    public class Builder
    {
        private readonly ReportCardLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new ReportCardLatexBuilder(templatesPath, outPath);
        }

        public ReportCardLatexBuilder build() => latexBuilder;

        public Builder withStudent(StudentEntity parameter)
        {
            latexBuilder.student = parameter;
            return this;
        }

        public Builder withTeacher(TeacherEntity parameter)
        {
            latexBuilder.teacher = parameter;
            return this;
        }
        
        public Builder withDegree(string parameter)
        {
            latexBuilder.degree = parameter;
            return this;
        }
        
        public Builder withSemesters(List<SemesterEntity> parameter)
        {
            latexBuilder._semesters = parameter;
            return this;
        }
        
        public Builder withSubjects(List<(string, string)> parameter)
        {
            latexBuilder.subjects = parameter;
            return this;
        }

        public Builder withLateArrivals(int parameter)
        {
            latexBuilder.lateArrivals = parameter.ToString();
            return this;
        }

        public Builder withUnjustifications(int parameter)
        {
            latexBuilder.unjustifications = parameter.ToString();
            return this;
        }

        public Builder withJustifications(int parameter)
        {
            latexBuilder.justifications = parameter.ToString();
            return this;
        }
    }
}