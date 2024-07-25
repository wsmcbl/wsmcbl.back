using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class BasicEnrollmentDto
{
    public string? enrollmentId { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }

    public BasicEnrollmentDto()
    {
    }

    public BasicEnrollmentDto(EnrollmentEntity enrollment)
    {
        enrollmentId = enrollment.enrollmentId;
        label = enrollment.label;
        section = enrollment.section;
        capacity = enrollment.capacity;
        quantity = enrollment.quantity;
    }
}