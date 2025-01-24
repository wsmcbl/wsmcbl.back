using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class OfficialEnrollmentListLatexBuilder(string templatesPath, string outPath) : LatexBuilder(templatesPath, outPath)
{
    // enrollment (lista )
    // teacher (list)
    // usuario que imprime
    //

    private string userName { get; set; } = null!;
    private string teacherName { get; set; } = null!;
    private List<DegreeEntity> degreeList { get; set; } = null!;
    
    protected override string getTemplateName() => "official-enrollment-list";

    protected override string updateContent(string content)
    {
        throw new NotImplementedException();
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

        public Builder withTeacherName(string parameter)
        {
            latexBuilder.teacherName = parameter;
            return this;
        }

        public Builder withUserName(string parameter)
        {
            latexBuilder.userName = parameter;
            return this;
        }
    }
}