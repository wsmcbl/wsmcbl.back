using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class StudentToEnrollDto : IBaseDto<StudentEntity>
{
    public string enrollmentId { get; set; }
    
    public StudentEntity toEntity()
    {
        throw new NotImplementedException();
    }
}