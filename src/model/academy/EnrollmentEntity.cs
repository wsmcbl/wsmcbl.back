using wsmcbl.src.model.accounting;

namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;

    public decimal grade { get; set; }

    public string section { get; set; } = null!;

    public virtual ICollection<StudentEntity> students { get; set; } = new List<StudentEntity>();
}