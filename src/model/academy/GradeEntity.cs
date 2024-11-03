namespace wsmcbl.src.model.academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public int subjectPartialId { get; set; }
    public string studentId { get; set; } = null!;
    public string? label { get; set; }
    public double? grade { get; set; }
    public double? conductGrade { get; set; }

    public void updateGrades(double? grade, double? conductGrade)
    {
        if (grade == null || conductGrade == null)
        {
            throw new ArgumentException("The values of grade and conductGrade must be not null.");
        }
        
        this.grade = grade;
        this.conductGrade = conductGrade;

        updateLabel();
    }

    private void updateLabel()
    {
        label = grade is >= 76 and < 90 ? "AS" : "AA";
        
        if (grade < 76)
        {
            label = grade >= 60 ? "AF" : "AI";
        }
    }
}