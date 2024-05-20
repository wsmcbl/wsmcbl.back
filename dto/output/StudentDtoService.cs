using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDtoService
{
    public List<StudentDtoToList> getStudentList(List<StudentEntity> students)
    {
        var studentDtos = new List<StudentDtoToList>();
        foreach (var student in students)
        {
           studentDtos.Add(new StudentDtoToList(student)); 
        }
        return studentDtos;
    }
}