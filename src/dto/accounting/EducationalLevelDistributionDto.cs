namespace wsmcbl.src.dto.accounting;

public class EducationalLevelDistributionDto
{
    public decimal tariffAmount { get; set; }
    public AmountByStudentQuantityDto total { get; set; } = null!;
    public AmountByStudentQuantityDto regularStudent { get; set; } = null!;
    public AmountByStudentQuantityDto discountedStudent { get; set; } = null!;
    public AmountByStudentQuantityDto scholarshipStudent { get; set; } = null!;
}