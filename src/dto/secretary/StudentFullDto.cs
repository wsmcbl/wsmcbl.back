using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentFullDto : IBaseDto<StudentEntity>
{
    [Required] public string studentId { get; set; } = null!;
    [Required] public string name { get; set; } = null!;
    [Required] public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    [Required] public string? secondSurname { get; set; }
    [JsonRequired] public bool sex { get; set; }
    [Required] public DateOnlyDto birthday { get; set; } = null!;
    [Required] public string religion { get; set; } = null!;
    [Required] public string? diseases { get; set; }
    
    [Required] public string address { get; set; } = null!;
    [JsonRequired] public bool isActive { get; set; }


    [JsonRequired] public StudentFileDto file { get; set; } = null!;
    [JsonRequired] public StudentTutorDto tutor { get; set; } = null!;
    [JsonRequired] public List<StudentParentDto>? parents { get; set; }
    [JsonRequired] public StudentMeasurementsDto measurements { get; set; } = null!;

    public StudentFullDto()
    {
    }

    public StudentFullDto(StudentEntity student)
    {
        studentId = student.studentId!;
        name = student.name;
        secondName = student.secondName;
        surname = student.surname;
        secondSurname = student.secondSurname;
        isActive = student.isActive;
        sex = student.sex;
        birthday = new DateOnlyDto(student.birthday);
        religion = student.religion;
        diseases = student.diseases;
        tutor = student.tutor.mapToDto();
        file = student.file.mapToDto();
        measurements = student.measurements.mapToDto();
        address = student.address;

        if (student.parents != null)
        {
            parents = student.parents.Select(e => e.mapToDto()).ToList();
        }
    }
    
    public StudentEntity toEntity()
    {
        return new StudentEntity.Builder()
            .setId(studentId)
            .setName(name)
            .setSecondName(secondName)
            .setSurname(surname)
            .setSecondSurname(secondSurname)
            .isActive(isActive)
            .setSex(sex)
            .setBirthday(birthday.toEntity())
            .setDiseases(diseases)
            .setReligion(religion)
            .setMeasurements(measurements.toEntity())
            .setParents(parents.toEntity())
            .setTutor(tutor.toEntity())
            .setFile(file.toEntity())
            .setAddress(address)
            .build();
    }
}