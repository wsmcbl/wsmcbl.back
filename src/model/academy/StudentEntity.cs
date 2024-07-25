using wsmcbl.src.model.secretary;

namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolYear { get; set; } = null!;
    public bool isApproved { get; set; }
    
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<ScoreEntity> scores { get; set; } = new List<ScoreEntity>();

    public class Builder
    {
        private readonly StudentEntity entity;

        public Builder()
        {
            entity = new StudentEntity();
        }

        public Builder(string studentId, string enrollmentId) : this()
        {
            entity.studentId = studentId;
            entity.enrollmentId = enrollmentId;
        }

        public StudentEntity build() => entity;

        public Builder isNewEnroll()
        {
            entity.isApproved = false;
            return this;
        }

        public Builder setSchoolyear(string schoolYearId)
        {
            entity.schoolYear = schoolYearId;
            return this;
        }
    }
}