namespace wsmcbl.back.model.accounting;

public class StudentEntity
{
    public string studentId;
    public string name { get; set; }
    public string? secondName { get; set; }
    public string surname { get; set; }
    public string? secondSurname { get; set; }
    public bool isActive { get; set; }
    public string schoolYear { get; set; }
    public string? tutor { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string? enrollmentLabel { get; set; }
    public DiscountEntity discount { get; set; }

    public ICollection<TransactionEntity> transactions { get; set; } = new List<TransactionEntity>();

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

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