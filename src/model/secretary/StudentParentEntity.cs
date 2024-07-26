namespace wsmcbl.src.model.secretary;

public class StudentParentEntity
{
    public string parentId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public bool sex { get; set; }
    public string name { get; set; } = null!;
    public string address { get; set; } = null!;
    public string? idCard { get; set; }
    public string? phone { get; set; }
    public string? occupation { get; set; }

    public void update(StudentParentEntity entity)
    {
        throw new NotImplementedException();
    }
}