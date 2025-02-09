using System.Globalization;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class DebtorReportLatexBuilder : LatexBuilder
{
    private DateTime now { get; set; }
    private string userName { get; set; } = null!;
    private List<DebtorStudentView>? studentList { get; set; }

    private readonly string templatesPath;

    private DebtorReportLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
        now = DateTime.UtcNow;
    }
    
    protected override string getTemplateName() => "debtor-report";

    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("total.value", getTotal().ToString(CultureInfo.InvariantCulture));
        content = content.ReplaceInLatexFormat("year.value", DateTime.Today.Year.ToString());
        content = content.ReplaceInLatexFormat("today.value", now.toDateUtc6());
        content = content.Replace("body.value", getDegreeContent());
        
        return content;
    }

    private double getTotal()
    {
        return studentList!.Sum(item => item.total);
    }

    private string getDegreeContent()
    {
        if (studentList!.Count == 0)
        {
            return "\\begin{center}\n\\textbf{\\large No hay deudores}\n\\end{center}\n";
        }

        var body = "\\begin{longtable}{| c || l || p{\\dimexpr\\textwidth-6cm\\relax} |}\n";
        body += "\\hline\\textbf{N\u00b0} & \\textbf{Código} & \\textbf{Nombre}\\\\\\hline\\hline\n";
        
        body += getEnrollmentContent(studentList);
        
        body += "\\end{longtable}\n\n";
        body += $"\\footnotetext{{Impreso por wsmcbl el {now.toStringUtc6(true)}, {userName}.}}\n";

        return body;
    }
    
    private string getEnrollmentContent(List<DebtorStudentView> list)
    {
        var counter = 0;
        var result = string.Empty;
        foreach (var item in list.OrderBy(e => e.fullName))
        {
            counter++;
            result += $"{counter} & {item.studentId} & {item.fullName}\\\\\\hline\n";
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

        public Builder withUserName(string parameter)
        {
            latexBuilder.userName = parameter;
            return this;
        }
    }
}