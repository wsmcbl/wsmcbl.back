using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.service.document;

public class DebtorReportLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private List<DebtorStudentView>? studentList { get; set; }
    
    protected override string getTemplateName() => "debtor-report";

    protected override string updateContent(string content)
    {
        throw new NotImplementedException();
    }

    public class Builder
    {
        private readonly DebtorReportLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new DebtorReportLatexBuilder(templatesPath, outPath);
        }

        public DebtorReportLatexBuilder build() => latexBuilder;

        public Builder withDegreeList(List<DebtorStudentView> parameter)
        {
            latexBuilder.studentList = parameter;
            return this;
        }
    }
}