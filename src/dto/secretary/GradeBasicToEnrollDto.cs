using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class GradeBasicToEnrollDto
{
    public string? gradeId { get; set; }
    public string label { get; set; } = null!;
    public string modality { get; set; } = null!;
    
    public ICollection<EnrollmentBasicDto> enrollments { get; set; }

    public GradeBasicToEnrollDto(){}

    public GradeBasicToEnrollDto(GradeEntity grade)
    {
        gradeId = grade.gradeId;
        label = grade.label;
        modality = grade.modality;
        enrollments = grade.enrollments.mapToListBasicDto();
    }
}