namespace wsmcbl.src.model.accounting;

public class DebtorStudentView
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public string schoolyear { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string enrollment { get; set; } = null!;
    public int quantity { get; set; }
    public decimal total {get; set;}
}