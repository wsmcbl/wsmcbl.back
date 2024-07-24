namespace wsmcbl.src.model.secretary;

public class StudentMeasurementsEntity
{
    public string measurementId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public double weight { get; set; }
    public int height { get; set; }
}