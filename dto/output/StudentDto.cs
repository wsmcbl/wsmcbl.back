namespace wsmcbl.back.dto.output;

public class StudentDto
{
    public string studentId { get; set; } = null!; 
    public string fullName { get; set; } = null!;
    public string? enrollmentLabel { get; set; }
    public string schoolYear { get; set; } = null!;
    public string? tutor { get; set; }
    public double discount { get; set; }
    public bool isActive { get; set; }
    public ICollection<TariffDto>? debtHistory { get; set; }
}