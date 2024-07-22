using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class StudentToEnrollDto : IBaseDto<StudentEntity>
{
    [Required] public string enrollmentId { get; set; } = null!;

    [Required] public string studentId { get; set; } = null!;
    [Required] public string name { get; set; } = null!;
    [Required] public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    [Required] public string? secondSurname { get; set; }
    [JsonRequired] public bool isActive { get; set; }
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string tutor { get; set; } = null!;
    [Required] public bool sex { get; set; }
    [Required] public DateOnlyDto birthday { get; set; } = null!;

    public StudentEntity toEntity()
    {
        return new StudentEntity();
    }
}