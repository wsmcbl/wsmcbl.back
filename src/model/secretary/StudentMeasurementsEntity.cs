namespace wsmcbl.src.model.secretary;

public class StudentMeasurementsEntity
{
    public int measurementId { get; set; }
    public string studentId { get; set; } = null!;
    public double weight { get; set; }
    public int height { get; set; }

    public StudentMeasurementsEntity(){}

    public StudentMeasurementsEntity(int measurementId, double weight, int height)
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