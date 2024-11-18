using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentFullDto : IBaseDto<StudentEntity>
{
    [Required] public string studentId { get; set; } = null!;
    public string? minedId { get; set; }
    [Required] public string name { get; set; } = null!;
    public string? secondName { get; set; }
    [Required] public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    [JsonRequired] public bool sex { get; set; }
    [Required] public DateOnlyDto birthday { get; set; } = null!;
    [Required] public string religion { get; set; } = null!;
    [Required] public string? diseases { get; set; }
    [Required] public string address { get; set; } = null!;
    [JsonRequired] public bool isActive { get; set; }
    public byte[]? profilePicture { get; set; }


    [JsonRequired] public StudentFileDto file { get; set; } = null!;
    [JsonRequired] public StudentTutorDto tutor { get; set; } = null!;
    [JsonRequired] public List<StudentParentDto>? parentList { get; set; }
    [JsonRequired] public StudentMeasurementsDto? measurements { get; set; }

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
        sex = student.sex;
        birthday = new DateOnlyDto(student.birthday);
        address = student.address;
        diseases = student.diseases;
        religion = student.religion;
        isActive = student.isActive;
        minedId = student.minedId;
        profilePicture = student.profilePicture;

        file = student.file.mapToDto();
        tutor = student.tutor.mapToDto();
        measurements = student.measurements.mapToDto();
        parentList = student.parents!.mapListToDto();
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
            .setAddress(address)
            .setMinedId(minedId)
            .setMeasurements(measurements?.toEntity())
            .setParents(parentList.toEntity())
            .setTutor(tutor.toEntity())
            .setFile(file.toEntity())
            .build();
    }
}