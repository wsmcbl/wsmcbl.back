namespace wsmcbl.src.dto.accounting;

public class ExpectedMonthlyReceivedDto
{
    public AmountByStudentQuantityDto total { get; set; } = null!;
    public EducationalLevelDistributionDto preschool { get; set; } = null!;
    public EducationalLevelDistributionDto elementary { get; set; } = null!;
    public EducationalLevelDistributionDto secondary { get; set; } = null!;
}