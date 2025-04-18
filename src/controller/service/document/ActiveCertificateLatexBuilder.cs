using System.Text;
using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;
namespace wsmcbl.src.controller.service.document;

public class ActiveCertificateLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private StudentEntity student { get; set; } = null!;
    private string degreeLabel { get; set; } = null!;
    private string? userAlias { get; set; }
    private string schoolyear { get; set; } = null!;

    protected override string getTemplateName() => "active-certificate";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo.png");
        
        content.Replace("schoolyear.value", schoolyear);
        content.Replace("degree.value", degreeLabel);
        
        content.Replace("student.id.value", student.studentId);
        content.Replace("student.name.value", student.fullName());
        
        content.Replace("secretary.name.value", userAlias != null ? $", {userAlias}" : string.Empty);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringUtc6(true));

        return content.ToString();
    }

    public class Builder
    {
        private readonly ActiveCertificateLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new ActiveCertificateLatexBuilder(templatesPath, outPath);
        }

        public ActiveCertificateLatexBuilder build() => latexBuilder;

        public Builder withStudent(StudentEntity parameter)
        {
            latexBuilder.student = parameter;
            return this;
        }
        
        public Builder withEnrollment(string parameter)
        {
            latexBuilder.degreeLabel = parameter;
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
    }
    
}