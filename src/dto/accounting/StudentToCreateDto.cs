using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.accounting;

public class StudentToCreateDto
{
    public string? studentId { get; set; }
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    [Required] public DateOnlyDto birthday { get; set; } = null!;
}