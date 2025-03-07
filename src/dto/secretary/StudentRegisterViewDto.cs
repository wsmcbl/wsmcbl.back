using wsmcbl.src.model.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.secretary;

public class StudentRegisterViewDto
{
    public string studentId { get; set; }
    public string minedId { get; set; }
    public string fullName { get; set; }
    public bool isActive { get; set; }
    public bool sex { get; set; }
    public string birthday { get; set; }
    public int age { get; set; }
    public string? diseases { get; set; }
    public string address { get; set; }
    public float height { get; set; }
    public float weight { get; set; }
    public string tutor { get; set; }
    public string phone { get; set; }
    public string father { get; set; }
    public string fatherIdCard { get; set; }
    public string mother { get; set; }
    public string motherIdCard { get; set; }
    public string schoolyear { get; set; }
    public string educationalLevel { get; set; }
    public string degree { get; set; }
    public string section { get; set; }
    public string enrollDate { get; set; }
    public bool isRepeating { get; set; }
    

    public StudentRegisterViewDto(StudentRegisterView value)
    {
        studentId = value.studentId;
        minedId = value.minedId.getOrDefault();
        fullName = value.fullName;
        isActive = value.isActive;
        sex = value.sex;
        birthday = value.birthday.ToString("dd-MM-yyyy");
        age = value.getAge();
        diseases = value.diseases;
        address = value.address;
        height = value.height;
        weight = value.weight;
        tutor = value.tutor;
        phone = value.phone.getOrDefault();
        father = value.father.getOrDefault();
        fatherIdCard = value.fatherIdCard.getOrDefault();
        mother = value.mother.getOrDefault();
        motherIdCard = value.motherIdCard.getOrDefault();
        schoolyear = value.schoolyear.getOrDefault();
        educationalLevel = value.educationalLevel.getOrDefault();
        degree = value.degree.getOrDefault();
        section = value.section.getOrDefault();
        enrollDate = value.enrollDate == null ? "N/A" : ((DateTime)value.enrollDate).ToString("dd-MM-yyy");
        isRepeating = value.isRepeating ?? false;
    }
}