using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class AccountingStudentDto
{
    public string studentId { get; set; } = null!; 
    public string fullName { get; set; } = null!;
    public string? enrollmentLabel { get; set; }
    public string? tutor { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public ICollection<PaymentItemDto>? paymentHistory { get; set; }

    public AccountingStudentDto()
    {
    }

    public AccountingStudentDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        enrollmentLabel = student.enrollmentLabel;
        tutor = student.tutor;
        discount = student.getDiscount();
        isActive = student.isActive;
        
        student.debtHistory ??= [];
        paymentHistory = student.debtHistory.Select(e => e.mapToDto()).ToList();
    }
}