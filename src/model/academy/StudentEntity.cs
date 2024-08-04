namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolYear { get; set; } = null!;
    public bool isApproved { get; set; }
    
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<GradeEntity>? scores { get; set; }
    
    public StudentEntity()
    {
    }

    public StudentEntity(string studentId, string enrollmentId)
    {
        this.studentId = studentId;
        this.enrollmentId = enrollmentId;
    }
    
    public void isNewEnroll()
    {
        isApproved = false;
    }

    public void setSchoolyear(string schoolYearId)
    {
        schoolYear = schoolYearId;
    }

    public string fullName()
    {
        return student.fullName();
    }
}