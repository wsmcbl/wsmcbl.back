using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.accounting;

public class TutorToCreateDto
{
    [Required] public string name { get; set; } = null!;
    [Required] public string phone { get; set; } = null!;
}