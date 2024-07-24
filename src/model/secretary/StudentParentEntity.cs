namespace wsmcbl.src.model.secretary;

public class StudentParentEntity
{
    public string Parentid { get; set; } = null!;

    public string Studentid { get; set; } = null!;

    public bool Type { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Idcard { get; set; }

    public string? Phone { get; set; }

    public string? Occupation { get; set; }
}