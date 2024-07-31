using System.Text.Json.Serialization;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentMeasurementsDto : IBaseDto<StudentMeasurementsEntity>
{
    [JsonRequired] public int measurementId { get; set; }
    [JsonRequired] public int height { get; set; }
    [JsonRequired] public double weight { get; set; }

    public StudentMeasurementsDto()
    {
    }

    public StudentMeasurementsDto(StudentMeasurementsEntity? entity)
    {
        if (entity == null)
        {
            return;
        }
        
        measurementId = entity.measurementId;
        height = entity.height;
        weight = entity.weight;
    }
    
    public StudentMeasurementsEntity toEntity()
    {
        return new StudentMeasurementsEntity(measurementId, weight, height);
    }
}