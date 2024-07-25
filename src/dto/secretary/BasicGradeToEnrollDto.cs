using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicGradeToEnrollDto
{
    public string? gradeId { get; set; }
    public string label { get; set; } = null!;
    public string modality { get; set; } = null!;
    
    public ICollection<BasicEnrollmentDto> enrollments { get; set; }

    public BasicGradeToEnrollDto(){}

    public BasicGradeToEnrollDto(GradeEntity grade)
    {
        gradeId = grade.gradeId;
        label = grade.label;
        modality = grade.modality;
        enrollments = grade.enrollments.mapToListBasicDto();
    }
}