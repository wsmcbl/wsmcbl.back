namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public StudentDto(string id, string fullName, string enrollment)
    {
        this.id = id;
        this.fullName = fullName;
        this.enrollment = enrollment;
    }

    public string id { get; set; }
    public string fullName { get; set; }
    public string enrollment { get; set; }
}