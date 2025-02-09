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
        var total = 10000.1;
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("total.value", total.ToString());
        content = content.ReplaceInLatexFormat("year.value", DateTime.Today.Year.ToString());

        var body = string.Empty;
        foreach (var item in studentList!.OrderBy(e => e.fullName))
        {
            body += getDegreeContent(item);
        }
        
        content = content.Replace("body.value", body);
        
        content += $"\\footnotetext{{Impreso por wsmcbl el {now.toStringUtc6(true)}, {userName}.}}\n";
        
        return content;
    }

    private string getDegreeContent(DebtorStudentView item)
    {
        return "";
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