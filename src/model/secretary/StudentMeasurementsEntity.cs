namespace wsmcbl.src.model.secretary;

public class StudentMeasurementsEntity
{
    public string measurementId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public double weight { get; set; }
    public int height { get; set; }

    public StudentMeasurementsEntity(){}

    public StudentMeasurementsEntity(string measurementId, double weight, int height)
    {
        this.measurementId = measurementId;
        this.weight = weight;
        this.height = height;
    }

    public void update(StudentMeasurementsEntity entity)
    {
        weight = entity.weight;
        height = entity.height;
    }
}