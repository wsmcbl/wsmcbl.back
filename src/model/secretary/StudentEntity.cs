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

        public Builder setId(string studentId)
        {
            entity.studentId = studentId;
            return this;
        }

        public Builder setName(string name)
        {
            entity.name = name;
            return this;
        }

        public Builder setSecondName(string? secondName)
        {
            entity.secondName = secondName;
            return this;
        }

        public Builder setSurname(string surname)
        {
            entity.surname = surname;
            return this;
        }

        public Builder setSecondSurname(string? secondSurname)
        {
            entity.secondSurname = secondSurname;
            return this;
        }

        public Builder isActive(bool isActive)
        {
            entity.isActive = isActive;
            return this;
        }

        public Builder setSchoolYear(string schoolYear)
        {
            entity.schoolYear = schoolYear;
            return this;
        }

        public Builder setTutor(string tutor)
        {
            entity.tutor = tutor;
            return this;
        }

        public Builder setSex(bool sex)
        {
            entity.sex = sex;
            return this;
        }

        public Builder setBirthday(DateOnly birthday)
        {
            entity.birthday = birthday;
            return this;
        }

        public Builder setRecord(StudentRecordEntity record)
        {
            entity.record = record;
            return this;
        }

        public Builder setContact(ContactInformationEntity contact)
        {
            entity.contact = contact;
            return this;
        }

        public Builder setPhysicalData(PhysicalDataEntity physicalData)
        {
            entity.physicalData = physicalData;
            return this;
        }
    }
}