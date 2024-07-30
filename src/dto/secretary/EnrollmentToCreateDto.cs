using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentToCreateDto
{
    [Required] public string gradeId { get; set; } = null!;
    [JsonRequired] public int quantity { get; set; }
}