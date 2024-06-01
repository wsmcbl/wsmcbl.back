using wsmcbl.back.database.map;

namespace wsmcbl.back.model.accounting;

public class StudentEntity
{
    public string studentId { get; set; }
    public int discountId { get; set; }
    public DiscountEntity discount { get; set; }
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<TransactionEntity> transactions { get; set; }
    
    public string fullName() => student.fullName();
    public string? enrollmentLabel => student.enrollmentLabel;
    public string schoolYear => student.schoolYear;
    public string? tutor => student.tutor;
    public bool isActive => student.isActive;

    public TransactionEntity getLastTransaction()
    {
        var transaction = transactions.FirstOrDefault();

        foreach (var item in transactions)
        {
            if (item.date >= transaction!.date)
            {
                transaction = item;
            }
        }
        
        return transaction!;
    }
}