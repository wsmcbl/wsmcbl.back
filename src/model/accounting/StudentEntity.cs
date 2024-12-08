namespace wsmcbl.src.model.accounting;

public class StudentEntity
{
    public string? studentId { get; set; }
    public int discountId { get; set; }
    public int educationalLevel { get; set; }
    public string? enrollmentLabel { get; set; }
    
    public DiscountEducationalLevelEntity? discount { get; set; }
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<TransactionEntity>? transactions { get; set; }
    public ICollection<DebtHistoryEntity>? debtHistory { get; set; }
    
    public string fullName() => student.fullName();
    public string tutor => student.getTutorName();
    public bool isActive => student.isActive;

    public StudentEntity()
    {
    }

    public void updateDiscountId(int value)
    {
        discountId = value switch
        {
            2 => educationalLevel + 3,
            3 => educationalLevel + 6,
            _ => discountId
        };
    }
    
    public int getDiscountIdFormat()
    {
        return discount!.getDiscountIdFormat();
    }
    
    public float getDiscount()
    {
        return discount != null ? discount!.amount : 0;
    }
    
    public float calculateDiscount(float amount)
    {
        return discount != null ? amount*getDiscount() : amount;
    }

    public async Task loadDebtHistory(IDebtHistoryDao dao)
    {
        debtHistory = await dao.getListByStudent(studentId!);
    }
}