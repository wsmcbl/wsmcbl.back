using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.academy;

public class StudentAverageDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }
    public List<GradeAverageDto> averageList {get; set;}
    public decimal? averageGrade { get; set; }

    public StudentAverageDto(StudentEntity parameter)
    {
        studentId = parameter.studentId;
        fullName = parameter.fullName();
        averageList = parameter.averageList!.Select(e => new GradeAverageDto(e)).ToList();
        averageGrade = parameter.computeAverageGrade().round();
    }
}