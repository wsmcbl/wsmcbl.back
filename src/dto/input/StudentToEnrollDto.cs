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

    [JsonRequired] public ContactDto contact { get; set; } = null!;
    [JsonRequired] public RecordDto record { get; set; } = null!;
    [JsonRequired] public PhysicalDataDto physicalData { get; set; } = null!;

    public StudentEntity toEntity()
    {
        return new StudentEntity.Builder()
            .setId(studentId)
            .setName(name)
            .setSecondName(secondName)
            .setSurname(surname)
            .setSecondSurname(secondSurname)
            .isActive(isActive)
            .setSchoolYear(schoolYear)
            .setTutor(tutor)
            .setSex(sex)
            .setBirthday(birthday.toEntity())
            .setContact(contact.toEntity())
            .setPhysicalData(physicalData.toEntity())
            .setRecord(record.toEntity())
            .build();
    }
}