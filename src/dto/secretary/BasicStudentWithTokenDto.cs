using wsmcbl.src.model;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.secretary;

public class BasicStudentWithTokenDto : BasicStudentDto
{
    public string accessToken { get; set; }
    
    public BasicStudentWithTokenDto(StudentView value, string? parameter) : base(value)
    {
        accessToken = parameter.getOrDefault();
    }
}