namespace wsmcbl.src.model.secretary;

public class StudentFileEntity
{
    public int fileId { get; set; }
    public string studentId { get; set; } = null!;
    public bool transferSheet { get; set; }
    public bool birthDocument { get; set; }
    public bool parentIdentifier { get; set; }
    public bool updatedGradeReport { get; set; }
    public bool conductDocument { get; set; }
    public bool financialSolvency { get; set; }

    public void update(StudentFileEntity entity)
    {
        transferSheet = entity.transferSheet;
        birthDocument = entity.birthDocument;
        parentIdentifier = entity.parentIdentifier;
        updatedGradeReport = entity.updatedGradeReport;
        conductDocument = entity.conductDocument;
        financialSolvency = entity.financialSolvency;
    }
}