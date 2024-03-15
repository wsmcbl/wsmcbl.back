namespace wsmcbl.back.model.accounting;

public class StudentEntity
{   
    public string studentId { get; set; }
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public string enrollment { get; set; }
    public string schoolYear { get; set; }
    public string tutor { get; set; }

    public string getId()
    {
        studentId = "2023-101001KJTC";
        return studentId;
    }

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }
}