using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentDto
{
    public string enrollmentId { get; set; }
    public string? section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }

    public EnrollmentDto()
    {
    }
    
    public EnrollmentDto(EnrollmentEntity entity)
    {
        enrollmentId = entity.enrollmentId;
        section = entity.section;
        capacity = entity.capacity;
        quantity = entity.quantity;
    }
}