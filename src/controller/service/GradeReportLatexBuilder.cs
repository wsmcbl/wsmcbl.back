using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service;

public class GradeReportLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student = null!;
    private TeacherEntity teacher = null!;
    private List<SemesterEntity> semesterList = null!;
    private List<(string initials, string subjectId)> subjectList = null!;
    
    private string degree = null!; 
    
    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.Replace("year.value", DateTime.Today.Year.ToString());
        content = content.Replace("student.name.value", student.fullName());
        content = content.Replace("teacher.name.value", teacher.fullName());
        content = content.Replace("degree.value", degree);
        content = content.Replace("column.format.value", getColumnQuantity());
        content = content.Replace("titleLine.value", getTitleLine());
        content = content.Replace("firstSemester.value", getFirstSemester());
        content = content.Replace("secondSemester.value", getSecondSemester());
        content = content.Replace("finalGrade.value", getFinalGrade());

        return content;
    }
    
    private string getColumnQuantity()
    {
        var quantity = (subjectList.Count + 1).ToString();
        return $"|*{{{quantity}}}{{c|}}";
    }
    private string getTitleLine()
    {
        var result = "Parcial";

        foreach (var item in subjectList)
        {
            result = $"{result} & {item.initials}";
        }

        return $"{result}\\\\";
    }

    private string getFirstSemester()
    {
        return getSemester(semesterList.First(e => e.semester == 1));
    }

    private string getSecondSemester()
    {
        return getSemester(semesterList.First(e => e.semester == 2));
    }
    
    private string getFinalGrade()
    {        
        var finalGrade = "NF";

        var quantity = subjectList.Count;
        
        while (quantity > 0)
        {
            finalGrade = $"{finalGrade} & ";
            quantity--;
        }

        return $"{finalGrade}\\\\ \\hline";
    }
    
    private string getSemester(SemesterEntity semester)
    {
        var firstPartial = student.partials!.First(e => e.partial == 1 && e.semesterId == semester.semesterId);
        var secondPartial = student.partials!.First(e => e.partial == 2 && e.semesterId == semester.semesterId);
        
        var semesterLine = semester.label;

        foreach (var unused in subjectList)
        {
            var firstGrade = 80;
            var secondGrade = 70;

            var grade = (firstGrade + secondGrade) / 2;
            semesterLine = $"{semesterLine} & {grade.ToString()}";
        }

        semesterLine = $"{semesterLine}\\\\ \\hline";
        
        return $"{getGradeByPartial(firstPartial)} {getGradeByPartial(secondPartial)} {semesterLine}";
    }

    private string getGradeByPartial(PartialEntity partial)
    {
        var labelLine = partial.label;
        var gradeLine = " ";

        foreach (var item in subjectList)
        {
            var result = partial.subjectPartialList!
                .FirstOrDefault(e => e.subjectId == item.subjectId);

            var label = result == null ? "" : result.studentGrade!.label;
            var grade = result == null ? "" : result.studentGrade!.grade.ToString();

            labelLine = $"{labelLine} & {label}";
            gradeLine = $"{gradeLine} & {grade}";
        }

        var quantity = (subjectList.Count + 1).ToString();
        labelLine = $"{labelLine}\\\\ \\cline{{2-{quantity}}}\n";
        gradeLine = $"{gradeLine}\\\\ \\hline \n";

        return $"{labelLine} {gradeLine}";
    }
    
    public class Builder
    {
        private readonly GradeReportLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new GradeReportLatexBuilder(templatesPath, outPath);
        }

        public GradeReportLatexBuilder build() => latexBuilder;

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
            latexBuilder.semesterList = parameter;
            return this;
        }
        
        public Builder withSubjects(List<(string, string)> parameter)
        {
            latexBuilder.subjectList = parameter;
            return this;
        }
    }
}