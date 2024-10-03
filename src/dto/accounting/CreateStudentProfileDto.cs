using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace wsmcbl.src.dto.accounting;

public class CreateStudentProfileDto
{
    [Required] public StudentToCreateDto student { get; set; }
    [Required] public TutorToCreateDto tutor { get; set; }
    [JsonRequired] public int modality { get; set; }
}