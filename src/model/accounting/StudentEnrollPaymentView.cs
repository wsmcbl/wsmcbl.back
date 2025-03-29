namespace wsmcbl.src.model.accounting;

public class StudentEnrollPaymentView 
{
    public string studentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public string? enrollmentId { get; set; }
}