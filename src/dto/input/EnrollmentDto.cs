using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.input;

public class EnrollmentDto : IBaseDto<EnrollmentEntity>
{
    [Required] public string? enrollmentId { get; set; }
    [Required] public string? label { get; set; }
    [Required] public string? schoolYear { get; set; }
    [Required] public string? section { get; set; }
    public int capacity { get; set; }
    
    public EnrollmentEntity toEntity()
    {
        var enrollment = new EnrollmentEntity
        {
            enrollmentId = enrollmentId!
        };
        
        enrollment.updateData(label!, schoolYear!, section!, capacity);
        
        return enrollment;
    }
}