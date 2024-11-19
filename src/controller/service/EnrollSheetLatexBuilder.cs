using System.Globalization;
using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.controller.service;

public class EnrollSheetLatexBuilder : LatexBuilder
{
    private string? grade;
    private string? userName;
    private readonly StudentEntity entity;
    private model.academy.StudentEntity? academyStudent;
    private readonly string templatesPath;
    
    public EnrollSheetLatexBuilder(string templatesPath, string outputPath, StudentEntity entity) : base(templatesPath, outputPath)
    {
        this.templatesPath = templatesPath;
        this.entity = entity;
    }

    public void setGrade(string value)
    {
        grade = value;
    }

    public void setUsername(string value)
    {
        userName = value;
    }
    
    public void setAcademyStudent(model.academy.StudentEntity student)
    {
        academyStudent = student;
    }
    
    protected override string getTemplateName() => "enroll-sheet";
    
    protected override string updateContent(string content)
    {
        content = content.ReplaceInLatexFormat("logo.value", $"{templatesPath}/image/cbl-logo-wb.png");
        content = content.ReplaceInLatexFormat("enroll.date.value", getDateFormat(academyStudent!.getCreateAtByDateOnly()));
        content = content.ReplaceInLatexFormat("student.name.value", entity.fullName());
        content = content.ReplaceInLatexFormat("degree.value", grade!);
        content = content.ReplaceInLatexFormat("repeating.value", getTextByBool(academyStudent!.isRepeating));
        content = content.ReplaceInLatexFormat("student.age.value", getAge(entity.birthday));
        content = content.ReplaceInLatexFormat("student.sex.value", getTextBySex(entity.sex));
        content = content.ReplaceInLatexFormat("student.birthday.value", getDateFormat(entity.birthday, false));
        content = content.ReplaceInLatexFormat("tutor.value", entity.tutor.name);
        content = content.ReplaceInLatexFormat("diseases.value", entity.diseases!);
        content = content.ReplaceInLatexFormat("phones.value", entity.tutor.phone);
        content = content.ReplaceInLatexFormat("email.value", entity.tutor.email);
        content = content.ReplaceInLatexFormat("religion.value", entity.religion);
        content = content.ReplaceInLatexFormat("address.value", entity.address);
        
        content = content.ReplaceInLatexFormat("secretary.name.value", userName);
        content = content.ReplaceInLatexFormat("current.datetime.value", getDateTimeNow());
        content = content.ReplaceInLatexFormat("student.id.value", entity.studentId);
        content = content.ReplaceInLatexFormat("student.token.value", entity.accessToken);

        content = setParents(content, entity.parents);
        content = setFile(content, entity.file!);
        
        content = content.ReplaceInLatexFormat($"current.year.value", getYearLabel());
        
        return content;
    }

    private static string getYearLabel()
    {
        var year = DateTime.Today.Month > 10 ? DateTime.Today.Year : DateTime.Today.Year - 1;
        return year.ToString();
    }

    private static string getDateFormat(DateOnly date, bool withDay = true)
    {
        var culture = new CultureInfo("es-ES");

        var format = withDay ? "dddd dd/MMM/yyyy" : "dd/MMMM/yyyy";
        return date.ToString(format, culture);
    }
    
    private static string getDateTimeNow()
    {
        var culture = new CultureInfo("es-ES")
        {
            DateTimeFormat =
            {
                AMDesignator = "am",
                PMDesignator = "pm"
            }
        };
        
        return DateTime.UtcNow.toUTC6().ToString("dddd dd/MMM/yyyy, hh:mm tt", culture);
    }

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
        content = content.ReplaceInLatexFormat($"{typeParent}.name.value", "");
        content = content.ReplaceInLatexFormat($"{typeParent}.idcard.value", "");
        content = content.ReplaceInLatexFormat($"{typeParent}.occupation.value", "");

        return content;
    }
    
    private static string setParent(string content, string typeParent, StudentParentEntity parent)
    {
        content = content.ReplaceInLatexFormat($"{typeParent}.name.value", parent.name);
        content = content.ReplaceInLatexFormat($"{typeParent}.idcard.value", parent.idCard!);
        content = content.ReplaceInLatexFormat($"{typeParent}.occupation.value", parent.occupation!);

        return content;
    }

    private static string setFile(string content, StudentFileEntity file)
    {
        content = content.ReplaceInLatexFormat($"birth.document.value", getTextByBool(file.birthDocument));
        content = content.ReplaceInLatexFormat($"conduct.document.value", getTextByBool(file.conductDocument));
        content = content.ReplaceInLatexFormat($"financial.solvency.value", getTextByBool(file.financialSolvency));
        content = content.ReplaceInLatexFormat($"parent.idcard.value", getTextByBool(file.parentIdentifier));
        content = content.ReplaceInLatexFormat($"transfer.sheet.value", getTextByBool(file.transferSheet));
        content = content.ReplaceInLatexFormat($"updated.grade.report.value", getTextByBool(file.updatedDegreeReport));

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

        return $"{age.ToString()} años";
    }
}
