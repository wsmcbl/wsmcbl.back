using System.Text;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;

namespace wsmcbl.src.controller.service.document;

public class AccountStatementLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private SchoolyearEntity schoolyear { get; set; } = null!;
    
    protected override string getTemplateName() => "account-statement";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo-wb.png");
        
        content.Replace("schoolyear.value", schoolyear.label);
        content.Replace("level.value", DegreeDataEntity.getLevelName(student.educationalLevel));
        
        content.Replace("student.id.value", student.studentId);
        content.Replace("student.name.value", student.fullName());
        
        content.Replace("body.value", getDebtBody());
        content.Replace("total.value", $"{total:#,0.00}");
        content.Replace("other.value", "");
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));
        content.Replace("current.date.value", DateTime.UtcNow.toDateUtc6());

        return content.ToString();
    }

    private decimal total { get; set; }
    private string getDebtBody()
    {
        var sb = new StringBuilder();
        var list = student.debtHistory!
            .Where(e => e.tariff.type == 1 && e.schoolyear == schoolyear.id);
        
        foreach (var item in list)
        {
            var tariff = item.tariff;
            sb.Append($"{tariff.concept} & {tariff.dueDate.toString()} & ");
            sb.Append($"{tariff.amount:#,0} & {item.calculateDiscount():#,0.00} &");
            sb.Append($"{item.arrears:#,0.00} & {item.amount:#,0.00} &");
            sb.Append($"{item.getDebtBalance():#,0.00} C\\$ \\\\ \n");
            total += item.getDebtBalance();
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
        
        public Builder withSchoolyear(SchoolyearEntity parameter)
        {
            latexBuilder.schoolyear = parameter;
            return this;
        }
    }
}