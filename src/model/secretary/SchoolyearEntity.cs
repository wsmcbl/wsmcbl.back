namespace wsmcbl.src.model.secretary;

public class SchoolyearEntity
{
    public string Schoolyearid { get; set; } = null!;

    public string Label { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly Deadline { get; set; }

    public bool Isactive { get; set; }

    public virtual ICollection<GradeEntity> Grades { get; set; } = new List<GradeEntity>();

    public virtual ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
}