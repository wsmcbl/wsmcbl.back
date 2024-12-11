namespace wsmcbl.src.model.academy;

public class SubjectPartialEntity
{
    public int subjectPartialId { get; set; }
    public string subjectId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public int partialId { get; set; }
    public string teacherId { get; set; } = null!;

    public ICollection<GradeEntity> gradeList { get; set; } = null!;
    public GradeEntity? studentGrade { get; set; }

    public void setStudentGrade(string studentId)
    {
        studentGrade = gradeList.FirstOrDefault(e => e.studentId == studentId);
    }
}