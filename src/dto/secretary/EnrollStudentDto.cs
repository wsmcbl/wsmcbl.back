using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class EnrollStudentDto
{
    [Required] public string? enrollmentId { get; set; }
    [JsonRequired] public int discountId { get; set; }
    [JsonRequired] public StudentFullDto student { get; set; } = null!;
    public IFormFile? profilePictureWritingValue { get; set; }

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

    public StudentEntity getStudent()
    {
        var result = student.toEntity();
        result.profileImage = getProfileImage().GetAwaiter().GetResult();
        
        return result;
    }
    
    private async Task<byte[]?> getProfileImage()
    {
        if (profilePictureWritingValue == null)
        {
            return null;
        }

        using var memoryStream = new MemoryStream();
        await profilePictureWritingValue.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}