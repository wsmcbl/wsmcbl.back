using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace wsmcbl.src.dto.input;

public class EnrollStudentDto
{
    [Required] public string enrollmentId { get; set; } = null!;
    [JsonRequired] public StudentFullDto student { get; set; }
}