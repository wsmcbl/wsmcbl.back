namespace wsmcbl.src.model.secretary;

public class StudentParentEntity
{
    public string? parentId { get; set; }
    public string studentId { get; set; } = null!;
    public string name { get; set; } = null!;
    public bool sex { get; set; }
    public string? idCard { get; set; }
    public string? occupation { get; set; }

    public void update(StudentParentEntity entity)
    {
        sex = entity.sex;
        name = entity.name;
        idCard = entity.idCard;
        occupation = entity.occupation;
    }
}