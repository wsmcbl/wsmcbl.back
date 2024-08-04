using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicGradeToEnrollDto
{
    public string gradeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string modality { get; set; } = null!;

    public ICollection<BasicEnrollmentDto>? enrollments { get; set; }

    public BasicGradeToEnrollDto()
    {
    }

    public BasicGradeToEnrollDto(DegreeEntity degree)
    {
        gradeId = degree.degreeId!;
        label = degree.label;
        modality = degree.modality;
        enrollments = degree.enrollments.mapToListBasicDto();
    }
}