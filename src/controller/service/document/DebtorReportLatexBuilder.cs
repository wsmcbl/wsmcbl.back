using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class DebtorReportLatexBuilder : LatexBuilder
{
    private DateTime now { get; set; }
    private string userName { get; set; } = null!;
    private List<DebtorStudentView> studentList { get; set; } = null!;
    private List<DegreeEntity> degreeList { get; set; } = null!;

    private readonly string templatesPath;

    private DegreeEntity? aDegree { get; set; }

    private DebtorReportLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
        now = DateTime.UtcNow;
    }

    protected override string getTemplateName() => "debtor-report";

    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("year.value", DateTime.Today.Year.ToString());
        content = content.ReplaceInLatexFormat("today.value", now.toDateUtc6());
        content = content.Replace("body.value", getDegreeContent());

        return content;
    }

    private decimal getTotal(int value)
    {
        if (value == 1)
        {
            return (decimal)studentList.Where(e => e.schoolyearId == aDegree!.schoolYear)
                .Sum(item => item.total);
        }

        if (value == 2)
        {
            return getTotal(0) - getTotal(1);
        }
        
        return (decimal)studentList.Sum(item => item.total);
    }

    private string getDegreeContent()
    {
        if (studentList.Count == 0)
        {
            return "\\begin{center}\n\\textbf{\\large No hay deudores}\n\\end{center}\n";
        }
        
        var std = studentList.First(e => e.schoolyearId == aDegree!.schoolYear);
        var body = $"\\textbf{{Año lectivo {std.schoolyear}}} \\hfill\\textbf{{Total:}} C\\$ {getTotal(1):N2}";
        body += "\\begin{longtable}{| l | l | l | c | l |}\n\\hline ";
        body +=
            "\\textbf{N\u00b0} & \\textbf{Código} & \\textbf{Nombre} & \\textbf{Cant.} & \\textbf{Total}\\\\\\hline\n";

        foreach (var degree in degreeList.OrderBy(e => e.tag))
        {
            foreach (var enrollment in degree.enrollmentList!.OrderBy(e => e.tag))
            {
                var list = studentList
                    .Where(e => e.enrollmentId == enrollment.enrollmentId).ToList();
                if (list.Count == 0)
                {
                    continue;
                }

                body += $"\\multicolumn{{5}}{{l}}{{\\textbf{{\\small -- {enrollment.label} --}}}}\\\\\\hline\n";
                body += getEnrollmentContent(list);
            }
        }
        body += "\\end{longtable}\n\n";
        
        body += getFromOtherSchoolyear();
        
        body += $"\\hfill\\textbf{{Super total:}} C\\$ {getTotal(0):N2}";
        body += $"\\footnotetext{{Impreso por wsmcbl el {now.toStringUtc6(true)}, {userName}.}}\n";

        return body;
    }
    
    private string getFromOtherSchoolyear()
    {
        var list = studentList.Where(e => e.schoolyearId != aDegree!.schoolYear).ToList();
        if (list.Count == 0)
        {
            return string.Empty;
        }
        
        var body = $"\\textbf{{Año lectivo anteriores}}\\hfill\\textbf{{Total:}} C\\$ {getTotal(2):N2}";
        body += "\\begin{longtable}{| l | l | l | c | l |}\n\\hline ";
        body += "\\textbf{N\u00b0} & \\textbf{Código} & \\textbf{Nombre} & \\textbf{Cant.} & \\textbf{Total}\\\\\\hline\n";
        body += getEnrollmentContent(list);
        body += "\\end{longtable}\n\n";

        return body;
    }

    private int counter;

    private string getEnrollmentContent(List<DebtorStudentView> list)
    {
        var result = string.Empty;
        foreach (var item in list.OrderBy(e => e.fullName))
        {
            counter++;
            result +=
                $"{counter} & {item.studentId} & {item.fullName} & {item.quantity} & C\\$ {item.total.ToString("N2")}\\\\\\hline\n";
        }

        return result;
    }

    public class Builder
    {
        private readonly DebtorReportLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new DebtorReportLatexBuilder(templatesPath, outPath);
        }

        public DebtorReportLatexBuilder build() => latexBuilder;

        public Builder withStudentList(List<DebtorStudentView> parameter)
        {
            latexBuilder.studentList = parameter;
            return this;
        }

        public Builder withDegreeList(List<DegreeEntity> parameter)
        {
            latexBuilder.degreeList = parameter;
            latexBuilder.aDegree = latexBuilder.degreeList.First();
            return this;
        }

        public Builder withUserName(string parameter)
        {
            latexBuilder.userName = parameter;
            return this;
        }
    }
}