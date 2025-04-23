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
        
        content.Replace("body.value", getDebtBody());
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));
        content.Replace("current.date.value", DateTime.UtcNow.toDateUtc6());

        return content.ToString();
    }

    private decimal total { get; set; }
    private string getDebtBody()
    {
        var sb = new StringBuilder();
        var list = student.debtHistory!.Where(e => e.tariff.type == 1);
        foreach (var item in list)
        {
            var tariff = item.tariff;
            sb.Append($"{tariff.tariffId} & {tariff.concept} & {tariff.dueDate} & ");
            sb.Append($"{tariff.amount:#,0} & {item.arrears:#,0} & {item.amount:#,0} C\\$ & {item.amount - item.debtBalance:#,0} C\\$ & ");
            sb.Append($"{(item.isPaid ? "C" : "P")} \\\\");
            sb.Append("\n");                        
            total += item.amount;
        }

        return sb.ToString();
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