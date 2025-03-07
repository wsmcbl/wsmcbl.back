namespace wsmcbl.src.model.secretary;

public class StudentRegisterView
{
    public string? studentId { get; set; }
    public string? minedId { get; set; }
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string? diseases { get; set; }
    public string address { get; set; } = null!;
    public float height { get; set; }
    public float weight { get; set; }
    public string tutor { get; set; } = null!;
    public string? phone { get; set; }
    public string? father { get; set; }
    public string? fatherIdCard {get; set;}
    public string? mother { get; set; }
    public string? motherIdCard {get; set;}
    public string? schoolyear { get; set; }
    public string? schoolyearId { get; set; }
    public string? educationalLevel { get; set; }
    public string? degree { get; set; }
    public int degreePosition { get; set; }
    public string? section { get; set; }
    public int sectionPosition { get; set; }
    public DateTime? enrollDate { get; set; }
    public bool? isRepeating { get; set; }

    public int getAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthday.Year;
        
        if (today < new DateOnly(today.Year, birthday.Month, birthday.Day))
        {
            age--;
        }

        return age;
    }
}