using wsmcbl.src.utilities;

namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolYear { get; set; } = null!;
    public bool isApproved { get; set; }
    public bool isRepeating { get; set; }
    public DateTime createdAt { get; set; }
    
    public secretary.StudentEntity student { get; init; } = null!;
    public List<PartialEntity>? partials { get; private set; }
    
    public StudentEntity()
    {
    }

    public StudentEntity(string studentId, string enrollmentId)
    {
        this.studentId = studentId;
        this.enrollmentId = enrollmentId;
        isApproved = false;
        createdAt = DateTime.UtcNow;
        isRepeating = false;
    }

    public void setSchoolyear(string schoolYearId)
    {
        schoolYear = schoolYearId;
    }

    public string fullName()
    {
        return student.fullName();
    }

    public void setPartials(List<PartialEntity> list)
    {
        partials = list;
    }

    public void setIsRepeating(bool repeating)
    {
        isRepeating = repeating;
    }

    public DateOnly getCreateAtByDateOnly()
    {
        return DateOnly.FromDateTime(createdAt.toUTC6());
    }
}