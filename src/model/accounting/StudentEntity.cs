namespace wsmcbl.src.model.accounting;

public class StudentEntity
{
    public StudentEntity()
    {
        transactions = [];
        debtHistory = [];
    }
    
    public string? studentId { get; set; }
    public int discountId { get; set; }
    public DiscountEntity? discount { get; set; }
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<TransactionEntity> transactions { get; set; }
    public ICollection<DebtHistoryEntity> debtHistory { get; set; }
    
    public string fullName() => student.fullName();
    public string? enrollmentLabel => student.enrollmentLabel;
    public string schoolYear => student.schoolYear;
    public string? tutor => student.tutor;
    public bool isActive => student.isActive;
}