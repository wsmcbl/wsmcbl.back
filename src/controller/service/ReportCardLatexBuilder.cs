using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class ReportCardLatexBuilder : LatexBuilder
{
    private readonly StudentEntity entity;
    public ReportCardLatexBuilder(string templatesPath, string outputPath, StudentEntity entity) : base(templatesPath, outputPath)
    {
        this.entity = entity;
    }

    protected override string getTemplateName() => "report-card";

    protected override string updateContent(string content)
    {
        content = content.Replace($"\\name", entity.fullName());

        return content;
    }
}