namespace wsmcbl.src.model.secretary;

public class StudentFileEntity
{
    public string fileId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public bool transferSheet { get; set; }
    public bool birthDocument { get; set; }
    public bool parentIdentifier { get; set; }
    public bool updatedGradeReport { get; set; }
    public bool conductDocument { get; set; }
    public bool financialSolvency { get; set; }
}