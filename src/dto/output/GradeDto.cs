using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.output;

public class GradeDto
{
    public string gradeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public string modality { get; set; }
    
    public List<EnrollmentEntity> enrollments { get; set; }
}