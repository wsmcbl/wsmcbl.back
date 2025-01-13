using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentToUpdateDto
{
    [Required] public string section { get; set; } = null!;
    [Required] public string label { get; set; } = null!;
    [JsonRequired] public int capacity { get; set; }
    [JsonRequired] public int quantity { get; set; }
    
    public EnrollmentEntity toEntity(string enrollmentId)
    {
        return new EnrollmentEntity
        {
            enrollmentId = enrollmentId,
            section = section,
            capacity = capacity,
            quantity = quantity,
            label = label
        };
    }
}