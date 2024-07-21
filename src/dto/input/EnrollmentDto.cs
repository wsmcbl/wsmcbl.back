using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.input;

public class EnrollmentDto : IBaseDto<EnrollmentEntity>
{
    [Required] public string? enrollmentId { get; set; }
    [Required] public string? section { get; set; }
    [JsonRequired] public int capacity { get; set; }
    [JsonRequired] public int quantity { get; set; }
    public List<SubjectEnrollDto> subjects { get; set; }
    
    public EnrollmentEntity toEntity()
    {
        var enrollment = new EnrollmentEntity
        {
            enrollmentId = enrollmentId!,
            section = section!,
            capacity = capacity,
            quantity = quantity
        };

        var subjectList = subjects.Select(item => item.toEntity(enrollmentId!)).ToList();

        enrollment.setSubjectList(subjectList);
        
        return enrollment;
    }
}