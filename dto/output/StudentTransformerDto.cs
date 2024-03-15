using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentTransformerDto
{
    public List<StudentDto> getStudentList(List<StudentEntity> students)
    {
        var studentDtos = new List<StudentDto>();
        foreach (var student in students)
        {
           studentDtos.Add(new StudentDto(student)); 
        }

        return studentDtos;
    }
}