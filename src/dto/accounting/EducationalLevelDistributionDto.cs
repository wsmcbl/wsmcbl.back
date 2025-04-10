using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class EducationalLevelDistributionDto
{
    public decimal tariffAmount { get; set; }
    public AmountByStudentQuantityDto total { get; set; }
    public AmountByStudentQuantityDto regularStudent { get; set; }
    public AmountByStudentQuantityDto discountedStudent { get; set; }
    public AmountByStudentQuantityDto scholarshipStudent { get; set; }

    public EducationalLevelDistributionDto(List<DebtHistoryEntity> parameter)
    {
        var first = parameter.FirstOrDefault();
        if (first != null)
        {
            tariffAmount = first.tariff.amount;
        }

        total = new AmountByStudentQuantityDto(parameter);
        regularStudent = new AmountByStudentQuantityDto(parameter);
        discountedStudent = new AmountByStudentQuantityDto(parameter);
        scholarshipStudent = new AmountByStudentQuantityDto(parameter);
    }
}