using System.Text.Json.Serialization;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class PhysicalDataDto : IBaseDto<PhysicalDataEntity>
{
    [JsonRequired] public int height { get; set; }
    [JsonRequired] public int weight { get; set; }
    
    public PhysicalDataEntity toEntity()
    {
        return new PhysicalDataEntity();
    }
}