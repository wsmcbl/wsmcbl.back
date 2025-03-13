using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class DegreeForCreateEnrollmentDto
{
    public string? degreeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public List<EnrollmentToCreateDto> enrollmentList { get; set; }

    public DegreeForCreateEnrollmentDto(DegreeEntity degree)
    {
        degreeId = degree.degreeId;
        label = degree.label;
        schoolYear = degree.schoolyearId;
        quantity = degree.quantity;
        
        enrollmentList = [];
        foreach (var enrollment in degree.enrollmentList!)
        {
            enrollmentList.Add(new EnrollmentToCreateDto(enrollment));
        }
    }
}