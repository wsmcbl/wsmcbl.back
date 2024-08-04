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
    
    public GradeDto(DegreeEntity degree)
    {
        gradeId = degree.degreeId;
        label = degree.label;
        schoolYear = degree.schoolYear;
        quantity = degree.quantity;
        modality = degree.modality;
        
        enrollments =  degree.enrollments == null || !degree.enrollments.Any() ?
            [] : degree.enrollments.mapListToDto();
        
        subjects = !degree.subjectList.Any() ? [] : degree.subjectList.mapListToDto();
    }
}