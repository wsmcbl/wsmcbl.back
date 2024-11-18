using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicDegreeToEnrollDto
{
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string educationalLevel { get; set; } = null!;
    public int position { get; set; }

    public ICollection<BasicEnrollmentDto>? enrollments { get; set; }

    public BasicDegreeToEnrollDto()
    {
    }

    public BasicDegreeToEnrollDto(DegreeEntity degree)
    {
        degreeId = degree.degreeId!;
        label = degree.label;
        educationalLevel = degree.educationalLevel;
        enrollments = degree.enrollmentList!.mapToListBasicDto();
        position = degree.getTag();
    }
}