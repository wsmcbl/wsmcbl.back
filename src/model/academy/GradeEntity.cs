namespace wsmcbl.src.model.academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public string studentId { get; set; } = null!;
    public string subjectId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public int partialId { get; set; }
    public string? label { get; set; }
    public double? grade { get; set; }
    public secretary.SubjectEntity secretarySubject { get; set; } = null!;

    public int getSemester()
    {
        return secretarySubject.semester;
    }
    
    public string getInitials()
    {
        return secretarySubject.initials;
    }

    private void updateLabel()
    {
        label = Utilities.getLabel((double)grade!);
    }
}