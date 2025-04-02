using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.academy;

public class StudentGradeSummaryDto
{
    public string studentId { get; set; }
    public string minedId { get; set; }
    public string fullName { get; set; }
    public bool sex { get; set; }
    public List<StudentGradeViewDto> gradeList {get; set;}
    public decimal gradeAverage { get; set; }
    public string labelAverage { get; set; }
    public decimal conductGrade { get; set; }
    public string conductGradeLabel { get; set; }

    public StudentGradeSummaryDto(StudentEntity parameter, int partial)
    {
        studentId = parameter.studentId;
        minedId = parameter.student.minedId.getOrDefault();
        fullName = parameter.fullName();
        sex = parameter.student.sex;
        
        gradeList = parameter.gradeList!.Select(e => new StudentGradeViewDto(e)).ToList();
        
        var average = parameter.getAverage(partial);
        gradeAverage = average.grade;
        labelAverage = average.getLabel();
        conductGrade = average.conductGrade;
        conductGradeLabel = average.getConductLabel();
    }
}