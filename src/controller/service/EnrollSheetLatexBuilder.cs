using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class EnrollSheetLatexBuilder : LatexBuilder
{
    private string? grade;
    private readonly StudentEntity entity;
    private readonly string templatesPath;
    
    public EnrollSheetLatexBuilder(string templatesPath, string outputPath, StudentEntity entity) : base(templatesPath, outputPath)
    {
        this.templatesPath = templatesPath;
        this.entity = entity;
        isRepetitive = false;
    }

    public void setGrade(string grade)
    {
        this.grade = grade;
    }
    protected override string getTemplateName() => "enroll-sheet";

    protected override string updateContent(string content)
    {
        content = content.Replace($"\\cbl-logo-wb", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.Replace($"\\date", DateTime.Today.Date.ToString("dd/MM/yyyy"));
        content = content.Replace($"\\name", entity.fullName());
        content = content.Replace($"\\grade", grade);
        content = content.Replace($"\\is.repetitive", getTextByBool(isRepetitive));
        content = content.Replace($"\\age", getAge(entity.birthday));
        content = content.Replace($"\\sex", getTextBySex(entity.sex));
        content = content.Replace($"\\birthday.day", entity.birthday.Day.ToString());
        content = content.Replace($"\\birthday.month", entity.birthday.Month.ToString());
        content = content.Replace($"\\birthday.year", entity.birthday.Year.ToString());
        content = content.Replace($"\\tutor", entity.tutor.name);
        content = content.Replace($"\\diseases", entity.diseases);
        content = content.Replace($"\\phones", entity.tutor.phone);
        content = content.Replace($"\\religion", entity.religion);
        content = content.Replace($"\\address", entity.address);

        content = setParents(content, entity.parents);
        content = setFile(content, entity.file!);
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        content = content.Replace($"\\current.year", today.Year.ToString());
        
        return content;
    }

    public bool isRepetitive { get; set; }

    private static string setParents(string content, ICollection<StudentParentEntity>? entityParents)
    {
        if (entityParents == null)
        {
            content = setParentNull(content, "father");
            content = setParentNull(content, "mother");
            return content;
        }
        
        var father = entityParents.FirstOrDefault(e => e.sex);
        content = father == null ? setParentNull(content, "father") : setParent(content, "father", father);
        
        var mother = entityParents.FirstOrDefault(e => !e.sex);
        content = mother == null ? setParentNull(content, "mother") : setParent(content, "mother", mother);
        
        return content;
    }

    private static string setParentNull(string content, string typeParent)
    {
        content = content.Replace($"\\{typeParent}.name", "");
        content = content.Replace($"\\{typeParent}.idcard", "");
        content = content.Replace($"\\{typeParent}.occupation", "");

        return content;
    }
    
    private static string setParent(string content, string typeParent, StudentParentEntity parent)
    {
        content = content.Replace($"\\{typeParent}.name", parent.name);
        content = content.Replace($"\\{typeParent}.idcard", parent.idCard);
        content = content.Replace($"\\{typeParent}.occupation", parent.occupation);

        return content;
    }

    private static string setFile(string content, StudentFileEntity file)
    {
        content = content.Replace($"\\birth.document", getTextByBool(file.birthDocument));
        content = content.Replace($"\\conduct.document", getTextByBool(file.conductDocument));
        content = content.Replace($"\\financial.solvency", getTextByBool(file.financialSolvency));
        content = content.Replace($"\\parent.idcard", getTextByBool(file.parentIdentifier));
        content = content.Replace($"\\transfer.sheet", getTextByBool(file.transferSheet));
        content = content.Replace($"\\updated.grade.report", getTextByBool(file.updatedDegreeReport));

        return content;
    }

    private static string getTextByBool(bool isSomething) => isSomething ? "Sí" : "No";
    private static string getTextBySex(bool sex) => sex ? "Hombre" : "Mujer";
    
    private static string getAge(DateOnly birthday)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthday.Year;

        if (today < birthday.AddYears(age))
        {
            age--;
        }

        return age.ToString();
    }
}