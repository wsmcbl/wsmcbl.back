using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;
using SubjectEntity = wsmcbl.src.model.academy.SubjectEntity;

namespace wsmcbl.src.controller.service.document;

public class ReportCardLatexBuilder : LatexBuilder
{
    private readonly string templatesPath;
 
    private ReportCardLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
    }
    
    private StudentEntity student { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;
    private List<SemesterEntity> semesterList { get; set; } = null!;
    private List<SubjectEntity> subjectList { get; set; } = null!;
    private List<SubjectAreaEntity> subjectAreaList { get; set; } = null!;
    
    private string degreeLabel { get; set; } = null!;
    private string? userName { get; set; }
    private string schoolyear { get; set; } = null!;

    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        
        content = content.Replace("schoolyear.value", schoolyear);
        content = content.Replace("degree.value", degreeLabel);
        
        content = content.Replace("student.id.value", student.studentId);
        content = content.Replace("student.name.value", student.fullName());
        content = content.Replace("teacher.name.value", teacher.fullName());
        
        content = content.Replace("detail.value", getDetail());

        var averageList = getAverageList();
        content = content.Replace("first.average.value", averageList[0]);
        content = content.Replace("second.average.value", averageList[1]);
        content = content.Replace("third.average.value", averageList[2]);
        content = content.Replace("fourth.average.value", averageList[3]);
        content = content.Replace("final.average.value", averageList[4]);
        
        content = content.ReplaceInLatexFormat("secretary.name.value", userName != null ? $", {userName}" : string.Empty);
        content = content.ReplaceInLatexFormat("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));

        return content;
    }

    private List<string> getAverageList()
    {
        var finalCounter = 0;
        decimal finalAccumulator = 0; 
        var result = new List<string>();
        foreach (var item in semesterList.OrderBy(e => e.semester))
        {
            foreach (var partial in student.partials!.Where(e => e.semester == item.semester).OrderBy(e => e.partial))
            {
                var counter = 0;
                decimal accumulator = 0;
                foreach (var subject in partial.subjectPartialList!)
                {
                    counter++;
                    subject.setStudentGrade(student.studentId);
                    accumulator += (decimal)subject.studentGrade!.grade!;
                }

                if (counter == 0)
                {
                    result.Add("");
                    continue;
                }

                var grade = accumulator / counter;
                result.Add($"{grade:F2}");

                finalCounter++;
                finalAccumulator += grade;
            }
        }

        if (finalCounter < 4)
        {
            result.Add("");
        }
        else
        {
            var grade = finalAccumulator / finalCounter;
            result.Add($"{grade:F2}");
        }
        
        return result;
    }

    private string getDetail()
    {
        var content = "";
        
        foreach (var item in subjectAreaList)
        {
            if (subjectList.All(e => e.secretarySubject!.areaId != item.areaId))
            {
                continue;
            }
            
            content += $"\\multicolumn{{11}}{{|l|}}{{\\textbf{{\\footnotesize {item.name}}}}}\\\\\\hline";
            content += getSubjectDetail(item.areaId);
        }

        return content;
    }

    private string getSubjectDetail(int areaId)
    {
        var content = "";
        foreach (var item in subjectList.Where(e => e.secretarySubject!.areaId == areaId))
        {
            content += $"{{\\footnotesize {item.secretarySubject!.name}}}";
            content += getGrades(item.subjectId);
            content += "\\\\\\hline";
        }

        return content;
    }

    private string getGrades(string subjectId)
    {
        var content = "";
        foreach (var item in semesterList.OrderBy(e => e.semester))
        {
            content += getGradesBySemester(item, subjectId);
        }

        content += getFinalGrade(subjectId);
        
        return content;
    }

    private string getFinalGrade(string subjectId)
    {
        var counter = 0;
        decimal accumulator = 0;
        foreach (var item in semesterList.OrderBy(e => e.semester))
        {
            foreach (var partial in student.partials!.Where(e => e.semester == item.semester).OrderBy(e => e.partial))
            {
                var result = partial.subjectPartialList!.FirstOrDefault(e => e.subjectId == subjectId);
                if (result == null) continue;
                
                counter++;
                result.setStudentGrade(student.studentId);
                accumulator += (decimal)result.studentGrade!.grade!;
            }
        }

        if (counter < 4) return " & & ";

        var grade = accumulator / counter;
        var label = GradeEntity.getLabelByGrade(grade);
        return $" & {label} & {grade}";
    }

    private string getGradesBySemester(SemesterEntity semester, string subjectId)
    {
        var content = "";
        foreach (var item in student.partials!.Where(e => e.semester == semester.semester).OrderBy(e => e.partial))
        {
            content += getGradesByPartial(item, subjectId);
        }
        
        return content;
    }

    private string getGradesByPartial(PartialEntity partial, string subjectId)
    {
        var result = partial.subjectPartialList!.FirstOrDefault(e => e.subjectId == subjectId);

        if (result != null)
        {
            result.setStudentGrade(student.studentId);
        }
            
        var label = result == null ? "" : result.studentGrade!.label;
        var grade = result == null ? "" : result.studentGrade!.grade.ToString();
        
        return $" & {label} & {grade}";
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
        
        public Builder withSemesterList(List<SemesterEntity> parameter)
        {
            latexBuilder.semesterList = parameter;
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
        
        public Builder withUsername(string? paramater)
        {
            latexBuilder.userName = paramater;
            return this;
        }
        
        public Builder withSchoolyear(string paramater)
        {
            latexBuilder.schoolyear = paramater;
            return this;
        }
    }
}