using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.accounting;

public class CreateStudentProfileDto
{
    [Required] public StudentToCreateDto student { get; set; } = null!;
    [Required] public TutorToCreateDto tutor { get; set; } = null!;
    [JsonRequired] public int educationalLevel { get; set; }

    public CreateStudentProfileDto()
    {
    }

    public CreateStudentProfileDto(StudentEntity studentEntity, int educationalLevel)
    {
        student = new StudentToCreateDto(studentEntity);
        tutor = new TutorToCreateDto(studentEntity.tutor!);
        this.educationalLevel = educationalLevel;
    }
}