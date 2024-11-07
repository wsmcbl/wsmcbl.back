using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicDegreeToEnrollDto
{
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string eduactionalLevel { get; set; } = null!;
    public int position { get; set; }

    public ICollection<BasicEnrollmentDto>? enrollments { get; set; }

    public BasicDegreeToEnrollDto()
    {
    }

    public BasicDegreeToEnrollDto(DegreeEntity degree)
    {
        degreeId = degree.degreeId!;
        label = degree.label;
        eduactionalLevel = degree.educationalLevel;
        enrollments = degree.enrollmentList!.mapToListBasicDto();

        try
        {
            position = Convert.ToInt32(degree.tag);
        }
        catch (Exception)
        {
            position = 1;
        }
    }
}