using wsmcbl.back.model.accounting;

namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }
    public string? enrollmentLabel { get; set; }
    public string schoolYear { get; set; }
    public string? tutor { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public ICollection<TransactionDto> transactions { get; set; }
}