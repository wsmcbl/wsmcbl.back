using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.output;

public class EnrollmentBasicDto
{
    public string? enrollmentId { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }

    public EnrollmentBasicDto()
    {
    }

    public EnrollmentBasicDto(EnrollmentEntity enrollment)
    {
        enrollmentId = enrollment.enrollmentId;
        label = enrollment.label;
        section = enrollment.section;
        capacity = enrollment.capacity;
        quantity = enrollment.quantity;
    }
}