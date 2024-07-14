namespace wsmcbl.src.model.secretary;

public class SchoolYearEntity
{
    public string schoolYearId { get; set; } = null!;

    public string label { get; set; } = null!;

    public DateOnly startDate { get; set; }

    public DateOnly deadLine { get; set; }

    public bool isActive { get; set; }
}