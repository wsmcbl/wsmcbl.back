namespace wsmcbl.src.model.secretary;

public class StudentEntity
{
    public string? studentId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool isActive { get; set; }
    public string schoolYear { get; set; } = null!;
    public string? tutor { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    
    public ContactInformationEntity contact { get; set; }
    public PhysicalDataEntity physicalData { get; set; }
    public StudentRecordEntity record { get; set; }

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

    public void init()
    {
        isActive = true;
        schoolYear = "";
    }
    
    public class Builder
    {
        private readonly StudentEntity entity;

        public Builder()
        {
            entity = new StudentEntity();
        }

        public StudentEntity build() => entity;

        public StudentEntity setId(string studentId)
        {
            entity.studentId = studentId;
            return entity;
        }

        public StudentEntity setName(string name)
        {
            entity.name = name;
            return entity;
        }

        public StudentEntity setSecondName(string secondName)
        {
            entity.secondName = secondName;
            return entity;
        }

        public StudentEntity setSurname(string surname)
        {
            entity.surname = surname;
            return entity;
        }

        public StudentEntity setSecondSurname(string secondSurname)
        {
            entity.secondSurname = secondSurname;
            return entity;
        }

        public StudentEntity isActive(bool isActive)
        {
            entity.isActive = isActive;
            return entity;
        }

        public StudentEntity setSchoolYear(string schoolYear)
        {
            entity.schoolYear = schoolYear;
            return entity;
        }

        public StudentEntity setTutor(string tutor)
        {
            entity.tutor = tutor;
            return entity;
        }

        public StudentEntity setSex(bool sex)
        {
            entity.sex = sex;
            return entity;
        }

        public StudentEntity setBirthday(DateOnly birthday)
        {
            entity.birthday = birthday;
            return entity;
        }

        public StudentEntity setRecord(StudentRecordEntity record)
        {
            entity.record = record;
            return entity;
        }

        public StudentEntity setContact(ContactInformationEntity contact)
        {
            entity.contact = contact;
            return entity;
        }

        public StudentEntity setPhysicalData(PhysicalDataEntity physicalData)
        {
            entity.physicalData = physicalData;
            return entity;
        }
    }
}