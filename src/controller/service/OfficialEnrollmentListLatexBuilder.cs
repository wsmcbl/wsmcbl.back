using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class OfficialEnrollmentListLatexBuilder : LatexBuilder
{
    private string userName { get; set; } = null!;
    private List<TeacherEntity> teacherList { get; set; } = null!;
    private List<DegreeEntity> degreeList { get; set; } = null!;
    

    private readonly string templatesPath;
    private OfficialEnrollmentListLatexBuilder(string templatesPath, string outPath) : base(templatesPath, outPath)
    {
        this.templatesPath = templatesPath;
    }
    
    protected override string getTemplateName() => "official-enrollment-list";

    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("date.value", DateTime.Now.ToString("dd-MM-yyyy"));

        return content;
    }

    
    public class Builder
    {
        private readonly OfficialEnrollmentListLatexBuilder latexBuilder;

        public Builder(string templatesPath, string outPath)
        {
            latexBuilder = new OfficialEnrollmentListLatexBuilder(templatesPath, outPath);
        }

        public OfficialEnrollmentListLatexBuilder build() => latexBuilder;

        public Builder withDegreeList(List<DegreeEntity> parameter)
        {
            latexBuilder.degreeList = parameter;
            return this;
        }

        public Builder withTeacherList(List<TeacherEntity> parameter)
        {
            latexBuilder.teacherList = parameter;
            return this;
        }

        public Builder withUserName(string parameter)
        {
            latexBuilder.userName = parameter;
            return this;
        }
    }
}