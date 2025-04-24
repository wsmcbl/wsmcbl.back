using System.Text;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class ProformaLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private string studentName { get; set; } = null!;
    private DegreeEntity degree { get; set; } = null!;
    private string? userAlias { get; set; }
    private string schoolyear { get; set; } = null!;

    private List<TariffEntity> tariffList { get; set; } = null!;

    protected override string getTemplateName() => "proforma";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo-wb.png");
        
        content.Replace("schoolyear.value", schoolyear);
        content.Replace("degree.value", degree.label.ToUpper());
        content.Replace("level.value", degree.educationalLevel);
        content.Replace("secretary.name.value", Utility.generalSecretary);
        
        content.Replace("student.name.value", studentName.ToUpper());

        content.Replace("body.value", getTariffBody());
        content.Replace("total.value", $"{total:#,0} C\\$");
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.date.value", DateTime.UtcNow.toString("d 'de' MMMM 'de' yyyy"));
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringFull());

        return content.ToString();
    }

    private decimal total { get; set; }
    private string getTariffBody()
    {
        var sb = new StringBuilder();
        foreach (var item in tariffList)
        {
            sb.Append($"{item.concept} & {item.amount:#,0} C\\$ \\\\");
            sb.Append("\n");
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

        public Builder withStudent(string parameter)
        {
            latexBuilder.studentName = parameter;
            return this;
        }
        
        public Builder withDegree(DegreeEntity parameter)
        {
            latexBuilder.degree = parameter;
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
        
        public Builder withTariffList(List<TariffEntity> parameter)
        {
            latexBuilder.tariffList = parameter;
            return this;
        }
    }
}