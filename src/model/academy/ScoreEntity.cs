namespace wsmcbl.src.model.academy;

public class ScoreEntity
{
    public int scoreId { get; set; }
    public string studentId { get; set; } = null!;
    public string subjectId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string? label { get; set; }
    public double? score { get; set; }
    
    public ICollection<ScoreItemEntity>? items { get; set; }
    public secretary.SubjectEntity secretarySubject { get; set; } = null!;

    public int getSemester()
    {
        return secretarySubject.semester;
    }
    
    public string getInitials()
    {
        return secretarySubject.initials;
    }

    public void calculateScore()
    {
        if (items == null)
        {
            throw new ArgumentException("Score Items collection must not be null");
        }
        
        score = 0;
        foreach (var item in items)
        {
            score += item.score;
        }

        score /= items.Count;
        updateLabel();
    }

    private void updateLabel()
    {
        label = Utilities.getLabel((double)score!);
    }
}