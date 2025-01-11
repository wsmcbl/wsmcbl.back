using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentToCreateDto
{
    [Required] public string enrollmentId { get; set; } = null!;
    [Required] public string label { get; set; } = null!;
    [Required] public string? section { get; set; }
    [JsonRequired] public int capacity { get; set; }

    public EnrollmentToCreateDto(EnrollmentEntity enrollment)
    {
        enrollmentId = enrollment.enrollmentId!;
        label = enrollment.label;
        section = enrollment.section;
        capacity = 0;
    }
}