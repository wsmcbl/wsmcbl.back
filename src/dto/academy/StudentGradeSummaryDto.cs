using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentGradeSummaryDto
{
    public string studentId { get; set; }
    public string minedId { get; set; }
    public string fullName { get; set; }
    public bool sex { get; set; }
    public List<StudentGradeViewDto> averageList {get; set;}
    public decimal grade { get; set; }
    public decimal label { get; set; }
    public decimal conductGrade { get; set; }

    public StudentGradeSummaryDto(StudentEntity parameter, int partial)
    {
        studentId = parameter.studentId;
        fullName = parameter.fullName();
        averageList = parameter.gradeList!.Select(e => new StudentGradeViewDto(e)).ToList();
        
        var average = parameter.getAverage(partial);
        grade = average.grade;
        label = average.getLabel();
        conductGrade = average.conductGrade;
    }
}