using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class ExpectedMonthlyReceivedDto
{
    public AmountByStudentQuantityDto total { get; set; }
    public AmountByStudentQuantityDto preschool { get; set; }
    public AmountByStudentQuantityDto elementary { get; set; }
    public AmountByStudentQuantityDto secondary { get; set; }
    
    public ExpectedMonthlyReceivedDto(List<DebtHistoryEntity> parameter)
    {
        total = new AmountByStudentQuantityDto(parameter);

        var list = parameter.Where(e => e.tariff.educationalLevel == 1).ToList();
        preschool = new AmountByStudentQuantityDto(list);
        
        list = parameter.Where(e => e.tariff.educationalLevel == 2).ToList();
        elementary = new AmountByStudentQuantityDto(list);
        
        list = parameter.Where(e => e.tariff.educationalLevel == 3).ToList();
        secondary = new AmountByStudentQuantityDto(list);
    }
}