using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class GradeDto
{
    public string? gradeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public string modality { get; set; }
    
    public List<EnrollmentDto> enrollments { get; set; }
    public List<SubjectDto>? subjects { get; set; }
    
    public GradeDto(GradeEntity grade)
    {
        gradeId = grade.gradeId;
        label = grade.label;
        schoolYear = grade.schoolYear;
        quantity = grade.quantity;
        modality = grade.modality;
        
        enrollments =  grade.enrollments == null || !grade.enrollments.Any() ?
            [] : grade.enrollments.mapListToDto();
        
        subjects = !grade.subjectList.Any() ? [] : grade.subjectList.mapListToDto();
    }
}