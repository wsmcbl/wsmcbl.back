using System.Text;
using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class AccountStatementLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    
    protected override string getTemplateName() => "account-statement";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo-wb.png");
        
        content.Replace("schoolyear.value", student.enrollmentLabel);
        content.Replace("level.value", model.secretary.DegreeDataEntity.getLevelName(student.educationalLevel));
        
        content.Replace("student.id.value", student.studentId);
        content.Replace("student.name.value", student.fullName());
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));
        content.Replace("current.date.value", DateTime.UtcNow.toDateUtc6());

        return content.ToString();
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