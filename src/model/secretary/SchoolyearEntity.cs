namespace wsmcbl.src.model.secretary;

public class SchoolyearEntity
{
    public string schoolYearId { get; set; } = null!;

    public string label { get; set; } = null!;

    public DateOnly startDate { get; set; }

    public DateOnly deadLine { get; set; }

    public bool isActive { get; set; }
    

    public ICollection<GradeEntity> grades { get; set; } = new List<GradeEntity>();

    public ICollection<StudentEntity> students { get; set; } = new List<StudentEntity>();
}