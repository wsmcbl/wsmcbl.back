using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class StudentDto : IBaseDto<StudentEntity>
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public required bool sex { get; set; }
    public DateOnlyDto birthday { get; set; } = null!;
    public string? tutor { get; set; }
    
    
    public StudentEntity toEntity()
    {       
        return new StudentEntity
        {
            name = name,
            secondName = secondName,
            surname = surname,
            secondSurname = secondSurname,
            sex = sex,
            birthday = birthday.toEntity(),
            tutor = tutor
        };
    }
}