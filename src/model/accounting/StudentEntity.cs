using wsmcbl.src.exception;

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

    public StudentEntity(string studentId, int educationalLevel)
    {
        if (string.IsNullOrWhiteSpace(studentId))
        {
            throw new IncorrectDataException("studentId", "value");
        }
        
        this.studentId = studentId;
        discountId = 1;
        this.educationalLevel = educationalLevel;
        enrollmentLabel = null;
    }
    
    public void updateDiscountId(int value)
    {
        discountId = value switch
        {
            2 => educationalLevel + 3,
            3 => educationalLevel + 6,
            _ => value
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

    public void updateEducationalLevel(int value)
    {
        if (educationalLevel == value)
        {
            throw new ConflictException("The student has the same level.");
        }
        
        educationalLevel = value;
    }

    public DebtHistoryEntity getCurrentRegistrationTariffDebt()
    {
        var result = debtHistory!.FirstOrDefault(e => e.tariff.type == Const.TARIFF_REGISTRATION);
        if (result == null)
        {
            throw new EntityNotFoundException("Registration tariff debt not found.");
        }

        return result;
    }

    public string getEducationalLevelLabel()
    {
        return educationalLevel switch
        {
            1 => "Preescolar",
            2 => "Primaria",
            3 => "Secundaria",
            _ => ""
        };
    }
}