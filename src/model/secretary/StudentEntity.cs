namespace wsmcbl.src.model.secretary;

public class StudentEntity
{
    public string? studentId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string? diseases { get; set; }
    public string address { get; set; } = null!;
    public string religion { get; set; } = null!;
    public bool isActive { get; set; }
    public string schoolYear { get; set; } = null!; 
    
    
    public StudentFileEntity file { get; set; } = null!;
    public StudentTutorEntity tutor { get; set; } = null!;
    public ICollection<StudentParentEntity>? parents { get; set; }
    public StudentMeasurementsEntity measurements { get; set; } = null!;

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

    public void init()
    {
        isActive = true;
        schoolYear = "";
    }

    public void update(StudentEntity entity)
    {
        name = entity.name;
        secondName = entity.secondName;
        surname = entity.surname;
        secondSurname = entity.secondSurname;
        isActive = entity.isActive;
        sex = entity.sex;
        birthday = entity.birthday;
        diseases = entity.diseases;
        religion = entity.religion;
    }
    
    public class Builder
    {
        private readonly StudentEntity entity;

        public Builder()
        {
            entity = new StudentEntity
            {
                parents = [],
                file = new StudentFileEntity(),
                tutor = new StudentTutorEntity(),
                measurements = new StudentMeasurementsEntity(),
                schoolYear = ""
            };
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

        public Builder setReligion(string religion)
        {
            entity.religion = religion;
            return this;
        }

        public Builder setDiseases(string? diseases)
        {
            entity.diseases = diseases;
            return this;
        }

        public Builder setFile(StudentFileEntity file)
        {
            file.studentId = entity.studentId!;
            entity.file = file;
            return this;
        }

        public Builder setTutor(StudentTutorEntity tutor)
        {
            tutor.studentId = entity.studentId!;
            entity.tutor = tutor;
            return this;
        }

        public Builder setMeasurements(StudentMeasurementsEntity studentMeasurements)
        {
            studentMeasurements.studentId = entity.studentId!;
            entity.measurements = studentMeasurements;
            return this;
        }

        public Builder setParents(List<StudentParentEntity> parents)
        {
            foreach (var item in parents)
            {
                item.studentId = entity.studentId!;
            }
            
            entity.parents = parents;
            return this;
        }

        public Builder setAddres(string address)
        {
            entity.address = address;
            return this;
        }
    }
}