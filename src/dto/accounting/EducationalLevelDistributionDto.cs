using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class EducationalLevelDistributionDto
{
    public decimal tariffAmount { get; set; }
    public AmountByStudentQuantityDto total { get; set; }
    public AmountByStudentQuantityDto regularStudent { get; set; }
    public AmountByStudentQuantityDto discountedStudent { get; set; }

    public EducationalLevelDistributionDto(List<DebtHistoryEntity> parameter)
    {
        var first = parameter.FirstOrDefault();
        if (first != null)
        {
            tariffAmount = first.tariff.amount;
        }

        total = new AmountByStudentQuantityDto(parameter);
        
        var list = parameter.Where(e => e.subAmount == tariffAmount).ToList();
        regularStudent = new AmountByStudentQuantityDto(list);
        
        list = parameter.Where(e => e.subAmount < tariffAmount).ToList();
        discountedStudent = new AmountByStudentQuantityDto(list);
    }
}