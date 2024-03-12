namespace wsmcbl.back.model.entity.accounting;

public class StudentEntity
{
    private string id;
    public string name { get; set; }
    public string lastName { get; set; }
    public string enrollment { get; set; }
    public string schoolYear { get; set; }
    public string tutor { get; set; }
    public List<string> disount { get; set; }
    public bool isBlacklisted { get; set; }

    public string getId()
    {
        id = "2023-101001KJTC";
        return id;
    }

    public string fullName()
    {
        return name + " " + lastName;
    }
}