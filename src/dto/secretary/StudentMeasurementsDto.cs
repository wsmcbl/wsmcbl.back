using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentMeasurementsDto : IBaseDto<StudentMeasurementsEntity>
{
    [Required] public string measurementId { get; set; } = null!;
    [JsonRequired] public int height { get; set; }
    [JsonRequired] public int weight { get; set; }
    
    public StudentMeasurementsEntity toEntity()
    {
        return new StudentMeasurementsEntity(measurementId, weight, height);
    }
}