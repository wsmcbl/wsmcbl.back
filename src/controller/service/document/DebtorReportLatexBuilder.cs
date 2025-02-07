namespace wsmcbl.src.controller.service.document;

public class DebtorReportLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    protected override string getTemplateName() => "debtor-report";

    protected override string updateContent(string content)
    {
        throw new NotImplementedException();
    }
}