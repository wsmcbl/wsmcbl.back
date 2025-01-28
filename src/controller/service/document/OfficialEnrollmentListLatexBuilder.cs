using System.Globalization;
using wsmcbl.src.model;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class OfficialEnrollmentListLatexBuilder : LatexBuilder
{
    private string userName { get; set; } = null!;
    private List<TeacherEntity> teacherList { get; set; } = null!;
    private List<DegreeEntity> degreeList { get; set; } = null!;
    private DateTime now { get; set; }

    private readonly string templatesPath;
    private OfficialEnrollmentListLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
        now = DateTime.UtcNow;
    }
    
    protected override string getTemplateName() => "official-enrollment-list";
    
    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("year.value", DateTime.Today.Year.ToString());

        var body = string.Empty;
        foreach (var item in degreeList.OrderBy(e => e.getTag()))
        {
            body += getDegreeContent(item);
        }
        
        content = content.Replace("body.value", body);
        
        return content;
    }

    private string getDegreeContent(DegreeEntity degree)
    {
        var result = string.Empty;
        
        var list = degree.enrollmentList!.Where(e => e.studentList!.Count != 0).ToList();
        foreach (var item in list.OrderBy(e => e.tag))
        {
            result += getEnrollmentContent(item);
        }
        
        return result;
    }

    private string getEnrollmentContent(EnrollmentEntity enrollment)
    {
        var result = $"\\begin{{center}}\n\\textbf{{\\large {enrollment.label}}}\n\\end{{center}}\n";
        result += $"\\textbf{{Docente guía}}: \\aField{{{getTeacherName(enrollment.teacherId)}}}";
        result += $"\\hfill \\textbf{{Fecha}}: {getDateFormat()}\n";
        result += $"\\footnotetext{{Impreso por wsmcbl el {now.toStringUtc6(true)}, {userName}.}}\n";

        result += "\\begin{longtable}{| c || l || p{\\dimexpr\\textwidth-6cm\\relax} |}\n";
        result += "\\hline\\textbf{N\u00b0} & \\textbf{Código} & \\textbf{Nombre}\\\\\\hline\\hline\n";
        
        var counter = 0;
        var womanList = enrollment.studentList!.Where(e => !e.student.sex).ToList();
        foreach (var item in womanList.OrderBy(e => e.fullName()))
        {
            counter++;
            result += $"{counter} & {item.studentId} & {item.fullName()}\\\\\\hline\n";
        }

        result += "\\hline\n";
        
        var menList = enrollment.studentList!.Where(e => e.student.sex).ToList();
        foreach (var item in menList.OrderBy(e => e.fullName()))
        {
            counter++;
            result += $"{counter} & {item.studentId} & {item.fullName()}\\\\\\hline\n";
        }
        
        result += "\\end{longtable}\n\n";
        result += "Si encuentra algún error, por favor repórtelo a secretaría para realizar la corrección.\n";
        result += $"Esta es la lista oficial de {enrollment.label}, si alguna hoja de matrícula no coincide, es posible que dicha hoja no esté actualizada.\n";
        
        if (counter > 24)
        {
            result += $"\\footnotetext{{Impreso por wsmcbl el {now.toStringUtc6(true)}, {userName}.}}\n";
        }
        
        result += "\\newpage\n";
        
        result += "\\setcounter{page}{1}\n\n\n";
        return result;
    }

    private string getTeacherName(string? teacherId)
    {
        var teacher = teacherList.FirstOrDefault(e => e.teacherId == teacherId);
        return teacher != null ? teacher.fullName() : "Sin asignar";
    }
    
    private string getDateFormat()
    {
        var culture = new CultureInfo("es-ES");
        return now.toUTC6().ToString("dd/MMMM/yyyy", culture);
    }

    public class Builder
    {
        private readonly OfficialEnrollmentListLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new OfficialEnrollmentListLatexBuilder(templatesPath, outPath);
        }

        public OfficialEnrollmentListLatexBuilder build() => latexBuilder;

        public Builder withDegreeList(List<DegreeEntity> parameter)
        {
            latexBuilder.degreeList = parameter;
            return this;
        }

        public Builder withTeacherList(List<TeacherEntity> parameter)
        {
            latexBuilder.teacherList = parameter.Where(e => e.teacherId != Const.DefaultTeacherId).ToList();
            return this;
        }

        public Builder withUserName(string parameter)
        {
            latexBuilder.userName = parameter;
            return this;
        }
    }
}