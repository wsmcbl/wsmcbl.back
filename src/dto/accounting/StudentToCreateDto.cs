using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.accounting;

public class StudentToCreateDto
{
    public string? studentId { get; set; }
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    [JsonRequired] public bool sex { get; set; }
    [Required] public DateOnlyDto birthday { get; set; } = null!;

    public StudentToCreateDto()
    {
    }

    public StudentToCreateDto(StudentEntity entity)
    {
        studentId = entity.studentId;
        name = entity.name;
        secondName = entity.secondName;
        surname = entity.surname;
        secondSurname = entity.secondSurname;
        sex = entity.sex;
        birthday = new DateOnlyDto(entity.birthday);
    }

    public StudentEntity toEntity()
    {
        return new StudentEntity.Builder()
            .setName(name)
            .setSecondName(secondName)
            .setSurname(surname)
            .setSecondSurname(secondSurname)
            .setBirthday(birthday.toEntity())
            .setSex(sex)
            .isActive(true)
            .build();
    }
}