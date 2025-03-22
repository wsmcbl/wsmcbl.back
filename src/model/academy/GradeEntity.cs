namespace wsmcbl.src.model.academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public int subjectPartialId { get; set; }
    public string studentId { get; set; } = null!;
    public string? label { get; set; }
    public decimal? grade { get; set; }
    public decimal? conductGrade { get; set; }

    public void updateGrades(decimal? gradeValue, decimal? conductGradeValue)
    {
        if (gradeValue == null || conductGradeValue == null)
        {
            throw new ArgumentException("The values of grade and conductGrade must be not null.");
        }
        
        grade = gradeValue;
        conductGrade = conductGradeValue;

        updateLabel();
    }

    private void updateLabel()
    {
        label = getLabelByGrade((decimal)grade!);
    }

    public static string getLabelByGrade(decimal value)
    {
        var result = value is >= 76 and < 90 ? "AS" : "AA";
        
        if (value < 76)
        {
            result = value >= 60 ? "AF" : "AI";
        }
        
        return result;
    }
}