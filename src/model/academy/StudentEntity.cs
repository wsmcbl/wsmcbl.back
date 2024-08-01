namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolYear { get; set; } = null!;
    public bool isApproved { get; set; }
    
    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<ScoreEntity>? scores { get; set; }

    public class Builder
    {
        private readonly StudentEntity entity;

        public Builder(string studentId, string enrollmentId)
        {
            entity = new StudentEntity
            {
                studentId = studentId,
                enrollmentId = enrollmentId
            };
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