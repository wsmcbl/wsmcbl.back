namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    public ReportCardLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
    }

    protected override string getTemplateName()
    {
        throw new NotImplementedException();
    }

    protected override string updateContent(string content)
    {
        throw new NotImplementedException();
    }
}