using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class RecordDto : IBaseDto<StudentRecordEntity>
{
    [JsonRequired] public bool haveTransferSheet { get; set; }
    [JsonRequired] public bool haveBirthDocument { get; set; }
    [JsonRequired] public bool haveParentIdentifier { get; set; }
    [JsonRequired] public bool haveUpdatedGradeReport { get; set; }
    [JsonRequired] public bool haveConductDocument { get; set; }
    [JsonRequired] public bool haveFinancialSolvency { get; set; }
    
    public StudentRecordEntity toEntity()
    {
        return new StudentRecordEntity();
    }
}