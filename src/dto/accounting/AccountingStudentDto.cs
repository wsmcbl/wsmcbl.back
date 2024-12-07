using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class AccountingStudentDto
{
    public string studentId { get; set; } 
    public string fullName { get; set; }
    public double discountId { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public ICollection<DebtDto>? debtList { get; set; }

    public AccountingStudentDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
        discountId = student.discountId;
        discount = student.getDiscount();
        isActive = student.isActive;

        debtList = student.debtHistory!.Select(e => new DebtDto(e)).ToList();
    }
}