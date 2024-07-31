using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentToUpdateDto : IBaseDto<EnrollmentEntity>
{
    [Required] public string enrollmentId { get; set; } = null!;
    public string? teacherId { get; set; }
    [Required] public string? section { get; set; }

    [Required] public string label { get; set; }
    [JsonRequired] public int capacity { get; set; }
    [JsonRequired] public int quantity { get; set; }

    public List<SubjectToAssignDto> subjects { get; set; } = null!;

    public EnrollmentToUpdateDto()
    {
    }
    
    public EnrollmentEntity toEntity()
    {
        var enrollment = new EnrollmentEntity
        {
            enrollmentId = enrollmentId,
            section = section!,
            capacity = capacity,
            quantity = quantity,
            label = label
        };

        var subjectList = subjects.Select(item => item.toEntity(enrollmentId)).ToList();
        enrollment.setSubjectList(subjectList);
        
        return enrollment;
    }
}