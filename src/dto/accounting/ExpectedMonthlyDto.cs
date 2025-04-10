using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class ExpectedMonthlyDto
{
    public AmountByStudentQuantityDto total { get; set; }
    public EducationalLevelDistributionDto preschool { get; set; }
    public EducationalLevelDistributionDto elementary { get; set; }
    public EducationalLevelDistributionDto secondary { get; set; }

    public ExpectedMonthlyDto(List<DebtHistoryEntity> parameter)
    {
        total = new AmountByStudentQuantityDto(parameter);

        var list = parameter.Where(e => e.tariff.educationalLevel == 1).ToList();
        preschool = new EducationalLevelDistributionDto(list);
        
        list = parameter.Where(e => e.tariff.educationalLevel == 2).ToList();
        elementary = new EducationalLevelDistributionDto(list);
        
        list = parameter.Where(e => e.tariff.educationalLevel == 3).ToList();
        secondary = new EducationalLevelDistributionDto(list);
    }
}