using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service.document;

public class AccountStatementLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    
    protected override string getTemplateName() => "account-statement";

    protected override string updateContent(string value)
    {
        throw new NotImplementedException();
    }
    
    public class Builder
    {
        private readonly AccountStatementLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new AccountStatementLatexBuilder(templatesPath, outPath);
        }

        public AccountStatementLatexBuilder build() => latexBuilder;

        public Builder withStudent(StudentEntity parameter)
        {
            latexBuilder.student = parameter;
            return this;
        }
        
        public Builder withUserAlias(string parameter)
        {
            latexBuilder.userAlias = parameter;
            return this;
        }
    }
}