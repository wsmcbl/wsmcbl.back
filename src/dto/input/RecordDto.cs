using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class RecordDto : IBaseDto<StudentFileEntity>
{
    [JsonRequired] public bool haveTransferSheet { get; set; }
    [JsonRequired] public bool haveBirthDocument { get; set; }
    [JsonRequired] public bool haveParentIdentifier { get; set; }
    [JsonRequired] public bool haveUpdatedGradeReport { get; set; }
    [JsonRequired] public bool haveConductDocument { get; set; }
    [JsonRequired] public bool haveFinancialSolvency { get; set; }
    
    public StudentFileEntity toEntity()
    {
        return new StudentFileEntity();
    }
}