using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class SubjectsToUpdateDto
{
    [Required]
    public int gradeId { get; set; }
    public List<string> subjectIdsList { get; set; } = null!;
}