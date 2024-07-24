namespace wsmcbl.src.model.secretary;

public class StudentMeasurementsEntity
{
    public string Measurementid { get; set; } = null!;

    public string Studentid { get; set; } = null!;

    public double Weight { get; set; }

    public int Height { get; set; }
}