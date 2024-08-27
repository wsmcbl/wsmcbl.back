using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicDegreeToEnrollDto
{
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string modality { get; set; } = null!;

    public ICollection<BasicEnrollmentDto>? enrollments { get; set; }

    public BasicDegreeToEnrollDto()
    {
    }

    public BasicDegreeToEnrollDto(DegreeEntity degree)
    {
        degreeId = degree.degreeId!;
        label = degree.label;
        modality = degree.modality;
        enrollments = degree.enrollmentList.mapToListBasicDto();
    }
}