using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class StudentDto
{
    public string studentId { get; set; } = null!; 
    public string fullName { get; set; } = null!;
    public string enrollmentLabel { get; set; } = null!;
    public string tutor { get; set; } = null!;
    public double discountId { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public string educationalLevel { get; set; } = null!;
    
    public ICollection<PaymentItemDto>? paymentHistory { get; set; }

    public StudentDto()
    {
    }

    public StudentDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        enrollmentLabel = student.enrollmentLabel!;
        tutor = student.tutor;
        discountId = student.getDiscountIdFormat();
        discount = student.getDiscount();
        isActive = student.isActive;
        educationalLevel = student.getEducationalLevelLabel();
        
        student.debtHistory ??= [];
        paymentHistory = student.debtHistory
            .Where(dh => dh.isPaid || dh.havePayments())
            .Select(e => e.mapToDto()).ToList();
    }
}