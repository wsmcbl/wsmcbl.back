namespace wsmcbl.src.model.secretary;

public class StudentFileEntity
{
    public string? fileId { get; set; }
    public bool haveTransferSheet { get; set; }
    public bool haveBirthDocument { get; set; }
    public bool haveParentIdentifier { get; set; }
    public bool haveUpdatedGradeReport { get; set; }
    public bool haveConductDocument { get; set; }
    public bool haveFinancialSolvency { get; set; }
}