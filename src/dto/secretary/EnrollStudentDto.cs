using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class EnrollStudentDto
{
    [Required] public string? enrollmentId { get; set; }
    [JsonRequired] public int discountId { get; set; }
    [JsonRequired] public StudentFullDto student { get; set; } = null!;

    public EnrollStudentDto()
    {
    }

    public EnrollStudentDto(StudentEntity student, (string? enrollmentId, int discountId) ids)
    {
        this.student = new StudentFullDto(student);
        enrollmentId = ids.enrollmentId;
        discountId = ids.discountId;
    }
    
    public string getStudentId()
    {
        return student.studentId;
    }
}