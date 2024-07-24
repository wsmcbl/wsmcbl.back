namespace wsmcbl.src.model.secretary;

public class StudentParentEntity
{
    public string parentId { get; set; } = null!;

    public string studentId { get; set; } = null!;

    public bool type { get; set; }

    public string name { get; set; } = null!;

    public string address { get; set; } = null!;

    public string? idCard { get; set; }

    public string? phone { get; set; }

    public string? occupation { get; set; }
}