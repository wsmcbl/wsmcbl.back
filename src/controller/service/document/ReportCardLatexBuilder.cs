using System.Text;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;
using SubjectEntity = wsmcbl.src.model.academy.SubjectEntity;

namespace wsmcbl.src.controller.service.document;

public class ReportCardLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private List<SubjectEntity> subjectList { get; set; } = null!;
    private List<SubjectAreaEntity> subjectAreaList { get; set; } = null!;
    private List<PartialEntity> partialList { get; set; } = null!;
    
    private string degreeLabel { get; set; } = null!;
    private string? userAlias { get; set; }
    private string schoolyear { get; set; } = null!;

    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo.png");
        
        content.Replace("schoolyear.value", schoolyear);
        content.Replace("degree.value", degreeLabel);
        
        content.Replace("student.id.value", student.studentId);
        content.Replace("student.name.value", student.fullName());
        content.Replace("teacher.name.value", teacher.fullName());
        content.Replace("teacher.mail.value", teacher.user.email);
        
        content.Replace("detail.value", getDetail());

        var averageList = getAverageList();
        content.Replace("first.average.value", averageList[0]);
        content.Replace("second.average.value", averageList[1]);
        content.Replace("third.average.value", averageList[2]);
        content.Replace("fourth.average.value", averageList[3]);
        content.Replace("final.average.value", averageList[4]);
        
        content.Replace("secretary.name.value", userAlias != null ? $", {userAlias}" : string.Empty);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringFull());

        return content.ToString();
    }

    private List<string> getAverageList()
    {
        var totalQuantity = 0;
        decimal totalValue = 0; 
        var result = new List<string>();

        foreach (var partial in partialList)
        {
            var quantity = 0;
            decimal value = 0;
            decimal conductValue = 0;
            
            foreach (var subject in partial.subjectPartialList!)
            {
                subject.setStudentGrade(student.studentId);
                if (subject.studentGrade != null)
                {
                    value += (decimal)subject.studentGrade!.grade!;
                    conductValue += (decimal)subject.studentGrade!.conductGrade!;
                }
                quantity++;
            }

            if (quantity == 0)
            {
                result.Add("");
                continue;
            }

            var conduct = conductValue/quantity;
            var grade = (value + conduct) / (quantity + 1);
            result.Add($"{grade:F2}");

            totalQuantity++;
            totalValue += grade;   
        }

        if (totalQuantity < 4)
        {
            result.Add("");
        }
        else
        {
            var grade = totalValue / totalQuantity;
            result.Add($"{grade:F2}");
        }
        
        return result;
    }

    private string getDetail()
    {
        var content = new StringBuilder();
        
        foreach (var item in subjectAreaList)
        {
            if (subjectList.All(e => e.secretarySubject!.areaId != item.areaId))
            {
                continue;
            }
            
            content.Append($"\\multicolumn{{11}}{{|l|}}{{\\textbf{{\\footnotesize {item.name}}}}}\\\\\\hline");
            getSubjectDetail(content, item.areaId);
        }

        addConductGrade(content);
        
        return content.ToString();
    }

    private void addConductGrade(StringBuilder content)
    {
        content.Append("{{\\footnotesize Conducta}}");

        var conduct = 0m;
        var quantity = 0;

        foreach (var partial in partialList)
        {
            var result = addConductGradeByPartial(content, partial);
            conduct += result.conduct;
            quantity += result.counter;            
        }

        content.Append(quantity != 4 ? gradeFormat(null) : gradeFormat(conduct/quantity));
        content.Append("\\\\\\hline");
    }

    private (decimal conduct, int counter) addConductGradeByPartial(StringBuilder content, PartialEntity partial)
    {
        if (partial.subjectPartialList!.Count == 0)
        {
            content.Append(gradeFormat(null));
            return (0, 0);
        }
        
        var conduct = 0m;
        var quantity = 0;

        foreach (var item in partial.subjectPartialList!)
        {
            item.setStudentGrade(student.studentId);
            if(item.studentGrade != null)
                conduct += (decimal)item.studentGrade!.conductGrade!;
            quantity++;
        }
        
        var grade = conduct/quantity;

        content.Append(gradeFormat(grade));
        return (grade, quantity > 0 ? 1 : 0);
    }

    private void getSubjectDetail(StringBuilder content, int areaId)
    {
        var list = subjectList.Where(e => e.secretarySubject!.areaId == areaId)
            .OrderBy(e => e.secretarySubject!.number).ToList();
            
        foreach (var item in list)
        {
            content.Append($"{{\\footnotesize {item.secretarySubject!.name}}}");
            getGrades(content, item.subjectId);
            content.Append("\\\\\\hline");
        }
    }

    private void getGrades(StringBuilder content, string subjectId)
    {
        foreach (var partial in partialList)
        {
            var result = partial.getSubjectPartialById(subjectId);
            if (result == null)
            {
                content.Append(gradeFormat(null));
                continue;
            }

            result.setStudentGrade(student.studentId);
            content.Append(gradeFormat(result.studentGrade?.grade, result.studentGrade?.label));
        }

        content.Append(getFinalGrade(subjectId));
    }

    private string getFinalGrade(string subjectId)
    {
        var grade = 0m;
        var quantity = 0;

        foreach (var partial in partialList)
        {
            var result = partial.getSubjectPartialById(subjectId);
            if (result == null) continue;
                
            quantity++;
            result.setStudentGrade(student.studentId);

            if (result.studentGrade != null)
            {
                grade += (decimal)result.studentGrade!.grade!;
            }
        }

        if (quantity != 2 && quantity != 4) return gradeFormat(null);

        return gradeFormat(grade/quantity);
    }
    
    private static string gradeFormat(decimal? grade, string? label = null)
    {
        if (grade == null)
        {
            return " & & ";
        }

        label ??= GradeEntity.getLabelByGrade((decimal)grade);
        
        return $" & {{\\footnotesize {label}}} & {{\\footnotesize {grade:F2}}}";
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
            latexBuilder.degreeLabel = parameter;
            return this;
        }
        
        public Builder withPartialList(List<PartialEntity> parameter)
        {
            latexBuilder.partialList = parameter
                .OrderBy(e => e.semester)
                .ThenBy(e => e.partial)
                .ToList();
            
            return this;
        }
        
        public Builder withSubjectList(List<SubjectEntity> parameter)
        {
            latexBuilder.subjectList = parameter;
            return this;
        }
        
        public Builder withSubjectAreaList(List<SubjectAreaEntity> parameter)
        {
            latexBuilder.subjectAreaList = parameter;
            return this;
        }
        
        public Builder withUserAlias(string? parameter)
        {
            latexBuilder.userAlias = parameter;
            return this;
        }
        
        public Builder withSchoolyear(string parameter)
        {
            latexBuilder.schoolyear = parameter;
            return this;
        }
    }
}