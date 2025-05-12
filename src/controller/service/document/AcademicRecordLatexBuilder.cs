using System.Text;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service.document;

public class AcademicRecordLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    private string userAlias { get; set; } = null!;
    private StudentEntity student { get; set; } = null!;
    private SchoolyearEntity schoolyear { get; set; } = null!;
    
    protected override string getTemplateName() => "academic-record";

    protected override string updateContent(string value)
    {
        var content = new StringBuilder(value);
        
        content.Replace("logo.value", $"{getImagesPath()}/cbl-logo-wb.png");
        
        content.Replace("schoolyear.value", schoolyear.label);
        
        content.Replace("student.id.value", student.studentId);
        content.Replace("student.name.value", student.fullName());
        
        content.Replace("other.value", "");
        
        content.Replace("user.alias.value", userAlias);
        content.Replace("current.datetime.value", DateTime.UtcNow.toStringFull());
        content.Replace("current.date.value", DateTime.UtcNow.toString());

        return content.ToString();
    }
    
    public class Builder
    {
        private readonly AcademicRecordLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new AcademicRecordLatexBuilder(templatesPath, outPath);
        }

        public AcademicRecordLatexBuilder build() => latexBuilder;

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