using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentToCreateDto
{
    [Required] public string enrollmentId { get; set; } = null!;
    public string? label { get; set; }
    [Required] public string section { get; set; } = null!;
    [JsonRequired] public int capacity { get; set; }

    public EnrollmentToCreateDto()
    {
    }
    
    public EnrollmentToCreateDto(EnrollmentEntity enrollment)
    {
        enrollmentId = enrollment.enrollmentId!;
        label = enrollment.label;
        section = enrollment.section;
        capacity = enrollment.capacity;
    }

    public EnrollmentEntity toEntity()
    {
        return new EnrollmentEntity
        {
            enrollmentId = enrollmentId,
            section = section,
            capacity = capacity
        };
    }
}