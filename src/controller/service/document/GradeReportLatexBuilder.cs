using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service.document;

public class GradeReportLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student = null!;
    private string teacherName = null!;
    private List<SemesterEntity> semesterList = null!;
    private List<PartialEntity> partialList = null!;
    private List<(string initials, string subjectId)> subjectList = null!;
    
    private string enroll = null!; 
    
    protected override string getTemplateName() => "grade-report";

    protected override string updateContent(string content)
    {
        content = content.Replace("year.value", DateTime.Today.Year.ToString());
        content = content.Replace("student.name.value", student.fullName());
        content = content.Replace("student.minedid.value", student.student.minedId);
        content = content.Replace("teacher.name.value", teacherName);
        content = content.Replace("degree.value", enroll);
        content = content.Replace("column.format.value", getColumnQuantity());
        content = content.Replace("titleLine.value", getTitleLine());
        content = content.Replace("firstSemester.value", getSemesterContent(1));
        content = content.Replace("secondSemester.value", getSemesterContent(2));
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
        var result = subjectList
            .Aggregate("Parcial", (current, item) => $"{current} & {item.initials}");

        return $@"{result}\\";
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

        return $@"{finalGrade}\\ \hline";
    }
    
    private string getSemesterContent(int type)
    {
        var semester = semesterList.First(e => e.semester == type);
        var firstPartial = partialList.First(e => e.partial == 1 && e.semesterId == semester.semesterId);
        var secondPartial = partialList.First(e => e.partial == 2 && e.semesterId == semester.semesterId);
        
        var semesterGrade = semester.label;

        foreach (var item in subjectList)
        {
            var firstGrade = firstPartial
                .subjectPartialList!
                .FirstOrDefault(e => e.subjectId.Equals(item.subjectId))?.studentGrade?.grade;
            
            var secondGrade = secondPartial
                .subjectPartialList!
                .FirstOrDefault(e => e.subjectId.Equals(item.subjectId))?.studentGrade?.grade;
            
            var grade = secondGrade == null ? firstGrade : (firstGrade + secondGrade) / 2;
            semesterGrade = $"{semesterGrade} & {grade.ToString()}";
        }

        semesterGrade = $"{semesterGrade}\\\\ \\hline";
        
        return $"{getGradeByPartial(firstPartial)} {getGradeByPartial(secondPartial)} {semesterGrade}";
    }

    private string getGradeByPartial(PartialEntity partial)
    {
        var labelLine = partial.label;
        var gradeLine = " ";
        
        foreach (var item in subjectList)
        {
            var subjectPartial = partial.subjectPartialList!
                .FirstOrDefault(e => e.subjectId.Equals(item.subjectId));

            var label = string.Empty;
            var grade = string.Empty;
            if (subjectPartial != null)
            {
                label = subjectPartial.studentGrade!.label;
                grade = subjectPartial.studentGrade.grade.ToString();   
            }

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

        public Builder withTeacherName(string parameter)
        {
            latexBuilder.teacherName = parameter;
            return this;
        }
        
        public Builder withEnroll(string parameter)
        {
            latexBuilder.enroll = parameter;
            return this;
        }
        
        public Builder withSemesterList(List<SemesterEntity> parameter)
        {
            latexBuilder.semesterList = parameter;
            return this;
        }
        
        public Builder withSubjectList(List<(string, string)> parameter)
        {
            latexBuilder.subjectList = parameter;
            return this;
        }

        public Builder withPartialList(List<PartialEntity> parameter)
        {
            latexBuilder.partialList = parameter;
            return this;
        }
    }
}