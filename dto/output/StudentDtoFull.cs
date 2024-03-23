using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDtoFull
{
    public string studentId { get;}
    public string fullName { get; }
    public string enrollmentLabel { get; }
    public string schoolyear { get; }
    public string tutor { get; }
    public float areas { get; set; }
    public float discount { get; set; }

    public ICollection<TransactionEntity> transactions { get; } = new List<TransactionEntity>();

    public StudentDtoFull(StudentEntity? student)
    {
        studentId = student.studentId;
        fullName = student.fullName();
        enrollmentLabel = student.enrollment;
        schoolyear = student.schoolYear;
        tutor = student.tutor;
        areas = 4;
        discount = (float)0.15;
        
        if (student.transactions.Count == 0)
        {
            var a = new TransactionEntity();
            a.studentId = "saf";
            a.cashierId = "saf";
            a.transactionId = "saf";
            transactions.Add(a);
        }
        else
        {
            transactions = student.transactions;
            
        }

    }
}