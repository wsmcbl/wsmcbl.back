using System.Text;
using wsmcbl.src.model.accounting;
using wsmcbl.src.utilities;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.controller.service.document;

public class ProformaLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;
    private string level { get; set; } = null!;
    private string? userAlias { get; set; }
    private string schoolyear { get; set; } = null!;

    private List<TariffEntity> tariffList { get; set; } = null!;

    protected override string getTemplateName() => "proforma";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo.png");
        
        content.Replace("schoolyear.value", schoolyear);
        content.Replace("enrollment.value", enrollmentLabel.ToUpper());
        content.Replace("level.value", level.ToUpper());
        
        content.Replace("mined.id.value", student.student.minedId.getOrDefault());
        content.Replace("student.name.value", student.fullName().ToUpper());

        content.Replace("body.value", getTariffBody());
        content.Replace("total.value", $"{total:F2}");
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.date.value", DateTime.UtcNow.toDateUtc6());
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));

        return content.ToString();
    }

    private decimal total { get; set; }
    private string getTariffBody()
    {
        var sb = new StringBuilder();
        foreach (var item in tariffList)
        {
            sb.Append($"{item.concept} & {item.amount:F2}\\\\\\hline ");
            total += item.amount;
        }

        return sb.ToString();
    }

    public class Builder
    {
        private readonly ProformaLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new ProformaLatexBuilder(templatesPath, outPath);
        }

        public ProformaLatexBuilder build() => latexBuilder;

        public Builder withStudent(StudentEntity parameter)
        {
            latexBuilder.student = parameter;
            return this;
        }
        
        public Builder withEnrollment(string parameter)
        {
            latexBuilder.enrollmentLabel = parameter;
            return this;
        }
        
        public Builder withUserAlias(string? parameter)
        {
            latexBuilder.userAlias = parameter;
            return this;
        }
        
        public Builder withSchoolyear(string parameter)
        {
            latexBuilder.schoolyear = parameter;
            return this;
        }
        
        public Builder withLevel(string parameter)
        {
            latexBuilder.level = parameter;
            return this;
        }
        
        public Builder withTariffList(List<TariffEntity> parameter)
        {
            latexBuilder.tariffList = parameter;
            return this;
        }
    }
}