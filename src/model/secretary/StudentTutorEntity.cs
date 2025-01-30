using System.Text.RegularExpressions;

namespace wsmcbl.src.model.secretary;

public class StudentTutorEntity
{
    public string? tutorId { get; set; }
    public string name { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string? email { get; set; }

    public StudentTutorEntity()
    {
    }
    
    public StudentTutorEntity(string name, string phone, string? tutorId = null, string? email = null)
    {
        this.tutorId = tutorId;
        this.name = name;
        this.phone = phone;
        this.email = email;
    }

    public bool isValidId()
    {
        return !string.IsNullOrWhiteSpace(tutorId);
    }
    
    public void update(StudentTutorEntity entity)
    {
        name = entity.name;
        phone = entity.phone;
        email = entity.email;
    }

    /// <summary>
    /// Check if there are multiple or a single phone number with its country code in the string.
    /// Examples
    /// +506 68451271
    /// 7845361
    /// 7845361, +50578451236
    /// </summary>
    public bool isValidPhone()
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7)
        {
            return false;
        }
        
        var value = phone[..8];
        return Regex.IsMatch(value, @"(?:\+\d{1,3}\s?)?\d{6,14}(?:,\s*(?:\+\d{1,3}\s?)?\d{6,14})*");
    }
}