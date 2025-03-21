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
    private List<SemesterEntity> semesterList { get; set; } = null!;
    private List<SubjectEntity> subjectList { get; set; } = null!;
    private List<SubjectAreaEntity> subjectAreaList { get; set; } = null!;
    private string principalName { get; set; } = null!;
    
    private DegreeEntity degree { get; set; } = null!;
    private string userName { get; set; } = null!;

    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.Replace("schoolyear.value", DateTime.Today.Year.ToString());
        content = content.Replace("degree.value", degree.label);
        
        content = content.Replace("mined.id.value", student.student.minedId);
        content = content.Replace("student.name.value", student.fullName());
        content = content.Replace("teacher.name.value", teacher.fullName());
        content = content.Replace("principal.name.value", principalName);
        content = content.Replace("educational.level.value", degree.getEducationalLevelToString());
        
        content = content.Replace("shift.value", "Matutino");
        content = content.Replace("department.value", "Managua");
        content = content.Replace("municipality.value", "Managua");
        content = content.Replace("school.id.value", "14484");
        
        content = content.Replace("detail.value", getDetail());

        var averageList = getAverageList();
        content = content.Replace("first.average.value", averageList[0]);
        content = content.Replace("second.average.value", averageList[1]);
        content = content.Replace("third.average.value", averageList[2]);
        content = content.Replace("fourth.average.value", averageList[3]);
        content = content.Replace("final.average.value", averageList[4]);
        
        content = content.ReplaceInLatexFormat("secretary.name.value", userName);
        content = content.ReplaceInLatexFormat("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));

        return content;
    }

    private List<string> getAverageList()
    {
        return ["81", "39", "75", "95", "100"];
    }

    private string getDetail()
    {
        var content = "";
        
        foreach (var item in subjectAreaList)
        {
            content += $"\\multicolumn{{11}}{{|l|}}{{\\textbf{{\\footnotesize {item.name}}}}}\\\\\\hline";
            content += getSubjectDetail(item.areaId);
            content += "\\hline";
        }

        return content;
    }

    private string getSubjectDetail(int areaId)
    {
        var content = "";
        foreach (var item in subjectList.Where(e => e.secretarySubject!.areaId == areaId))
        {
            content += $"{{\\footnotesize {item.secretarySubject!.name}";
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
        
        return content;
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
        
        public Builder withDegree(DegreeEntity parameter)
        {
            latexBuilder.degree = parameter;
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
        
        public Builder withPrincipalName(string parameter)
        {
            latexBuilder.principalName = parameter;
            return this;
        }
        
        public Builder withUsername(string paramater)
        {
            latexBuilder.userName = paramater;
            return this;
        }
    }
}