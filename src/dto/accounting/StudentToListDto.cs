using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class StudentToListDto
{
    public string studentId { get; set; } = null!; 
    public string fullName { get; set; } = null!;
    public string? enrollmentLabel { get; set; }
    public string? tutor { get; set; }
    public double discountId { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public ICollection<PaymentItemDto>? paymentHistory { get; set; }

    public StudentToListDto()
    {
    }

    public StudentToListDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        enrollmentLabel = student.enrollmentLabel;
        tutor = student.tutor;
        discountId = student.getDiscountIdFormat();
        discount = student.getDiscount();
        isActive = student.isActive;
        
        student.debtHistory ??= [];
        paymentHistory = student.debtHistory
            .Where(dh => dh.isPaid || dh.havePayments())
            .Select(e => e.mapToDto()).ToList();
    }
}