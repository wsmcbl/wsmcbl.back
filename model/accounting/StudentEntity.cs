namespace wsmcbl.back.model.accounting;

public class StudentEntity
{
    public string studentId;
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public string enrollment { get; set; }
    public string schoolYear { get; set; }
    public string tutor { get; set; }

    public ICollection<TransactionEntity> transactions { get; set; } = new List<TransactionEntity>();

    public string getStudentId()
    {
        studentId = getCurrentYear() + "-" + counter() + name[0] + secondName[0] + surname[0] + secondSurname[0];
        return studentId;
    }

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

    private int counter()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        return random.Next(1000, 9999);
    }

    private int getCurrentYear()
    {
        var time = new DateTime();
        return time.Date.Year;
    }

    public TransactionEntity getLastTransaction()
    {
        var transa = transactions.FirstOrDefault()!;
        
        foreach (var item in transactions)
        {
            if (item.dateTime >= transa.dateTime)
            {
                transa = item;
            }
        }
        
        return transa;
    }
}