using wsmcbl.src.exception;
using wsmcbl.src.utilities;

namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public bool isApproved { get; set; }
    public bool isRepeating { get; set; }
    public DateTime createdAt { get; set; }
    public string? enrollmentLabel { get; set; }
    
    public secretary.StudentEntity student { get; set; } = null!;
    public List<PartialEntity>? partials { get; set; }
    public List<GradeView>? gradeList { get; set; }
    public List<GradeAverageView>? averageList { get; set; }
    
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
        schoolyearId = schoolYearId;
    }

    public string fullName()
    {
        return student.fullName();
    }

    public void setIsRepeating(bool repeating)
    {
        isRepeating = repeating;
    }

    public DateOnly getCreateAtByDateOnly()
    {
        return DateOnly.FromDateTime(createdAt.toUTC6());
    }

    public decimal? computeFinalGrade()
    {
        if (averageList is not { Count: 4 })
        {
            return null;
        }

        return averageList.Sum(item => item.grade) / 4;
    }

    public GradeAverageView getAverage(int partial)
    {
        if (averageList == null)
        {
            throw new InternalException("The averageList must be not null.");
        }
        
        var result = averageList.FirstOrDefault(e => e.partial == partial);
        if (result == null)
        {
            throw new EntityNotFoundException($"The GradeAverageEntity for partial ({partial}) in StudentEntity not found.");
        }

        return result;
    }

    public bool passedAllSubjects()
    {
        return gradeList!.All(e => e.grade >= 60);
    }

    public bool isFailed(int type)
    {
        var count = gradeList!.Count(e => e.grade < 60);
        return type == 1 ? count <= 2 : count > 2;
    }

    public bool hasNotEvaluated()
    {
        return gradeList!.All(e => e.grade == 0 && e.conductGrade == 0);
    }

    public bool isWithInRange(string label, int partial)
    {
        var average = getAverage(partial);
        return GradeEntity.getLabelByGrade(average.grade).Equals(label); 
    }
}