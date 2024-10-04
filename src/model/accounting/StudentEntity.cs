namespace wsmcbl.src.model.accounting;

public class StudentEntity
{
    public string? studentId { get; set; }
    public int discountId { get; set; }
    public int educationalLevel { get; set; }
    public string? enrollmentLabel { get; set; }
    
    public DiscountEntity? discount { get; set; }
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<TransactionEntity>? transactions { get; set; }
    public ICollection<DebtHistoryEntity>? debtHistory { get; set; }
    
    public string fullName() => student.fullName();
    public string schoolYear => "Por implementar";
    public string tutor => student.tutor.name;
    public bool isActive => student.isActive;

    public StudentEntity()
    {
    }

    public float getDiscount()
    {
        if (discount == null)
        {
            return 0;
        }
        
        return discount!.amount;
    }
    
    public float calculateDiscount(float amount)
    {
        if (discount == null)
        {
            return amount;
        }
        
        return amount*getDiscount();
    }

    public async Task loadDebtHistory(IDebtHistoryDao? dao)
    {
        if (dao == null)
        {
            return;
        }
        
        debtHistory = await dao.getListByStudent(studentId!);
    }
}