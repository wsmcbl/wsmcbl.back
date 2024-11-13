using System.Globalization;
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
        isNewEnroll = true;
    }

    public void setGrade(string grade)
    {
        this.grade = grade;
    }
    protected override string getTemplateName() => "enroll-sheet";

    protected override string updateContent(string content)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        content = content.Replace($"logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.Replace($"today.value", getDate(today));
        content = content.Replace($"student.name.value", entity.fullName().ToUpper());
        content = content.Replace($"degree.value", grade.ToUpper());
        content = content.Replace($"enroll.record.value", getTextByEnrollRecord().ToUpper());
        content = content.Replace($"student.age.value", getAge(entity.birthday).ToUpper());
        content = content.Replace($"student.sex.value", getTextBySex(entity.sex).ToUpper());
        content = content.Replace($"student.birthday.value", getDate(entity.birthday, false).ToUpper());
        content = content.Replace($"tutor.value", entity.tutor.name.ToUpper());
        content = content.Replace($"diseases.value", entity.diseases.ToUpper());
        content = content.Replace($"phones.value", entity.tutor.phone);
        content = content.Replace($"email.value", "");
        content = content.Replace($"religion.value", entity.religion.ToUpper());
        content = content.Replace($"address.value", entity.address.ToUpper());

        content = setParents(content, entity.parents);
        content = setFile(content, entity.file!);
        
        content = content.Replace($"current.year.value", today.Year.ToString());
        
        return content;
    }
    
    private  string getDate(DateOnly date, bool withDay = true)
    {
        var culture = new CultureInfo("es-ES");

        var format = withDay ? "dddd dd/MMM/yyyy" : "dd/MMMM/yyyy";
        return date.ToString(format, culture);
    }

    private bool isNewEnroll { get; set; }

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
        content = content.Replace($"{typeParent}.name.value", "");
        content = content.Replace($"{typeParent}.idcard.value", "");
        content = content.Replace($"{typeParent}.occupation.value", "");

        return content;
    }
    
    private static string setParent(string content, string typeParent, StudentParentEntity parent)
    {
        content = content.Replace($"{typeParent}.name.value", parent.name.ToUpper());
        content = content.Replace($"{typeParent}.idcard.value", parent.idCard.ToUpper());
        content = content.Replace($"{typeParent}.occupation.value", parent.occupation.ToUpper());

        return content;
    }

    private static string setFile(string content, StudentFileEntity file)
    {
        content = content.Replace($"birth.document.value", getTextByBool(file.birthDocument));
        content = content.Replace($"conduct.document.value", getTextByBool(file.conductDocument));
        content = content.Replace($"financial.solvency.value", getTextByBool(file.financialSolvency));
        content = content.Replace($"parent.idcard.value", getTextByBool(file.parentIdentifier));
        content = content.Replace($"transfer.sheet.value", getTextByBool(file.transferSheet));
        content = content.Replace($"updated.grade.report.value", getTextByBool(file.updatedDegreeReport));

        return content;
    }

    private static string getTextByBool(bool isSomething) => isSomething ? "Sí" : "No";
    private static string getTextBySex(bool sex) => sex ? "Hombre" : "Mujer";
    private string getTextByEnrollRecord() => isNewEnroll ? "Primer ingreso" : "Reingreso";
    
    private static string getAge(DateOnly birthday)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthday.Year;

        if (today < birthday.AddYears(age))
        {
            age--;
        }

        return $"{age.ToString()} años";
    }
}
