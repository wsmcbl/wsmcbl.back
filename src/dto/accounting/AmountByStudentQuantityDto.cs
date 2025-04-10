using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class AmountByStudentQuantityDto
{
    public decimal amount { get; set; }
    public int studentQuantity { get; set; }

    public AmountByStudentQuantityDto(List<DebtHistoryEntity> parameter)
    {
        amount = parameter.Sum(e => e.amount);
        studentQuantity = parameter.Select(e => e.studentId).Distinct().Count();
    }
}