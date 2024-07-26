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
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public string? religion { get; set; }
    public string? diseases { get; set; } 
    
    
    public ICollection<StudentParentEntity>? parents { get; set; }
    public StudentTutorEntity? tutor { get; set; }
    public StudentMeasurementsEntity? measurements { get; set; }
    public StudentFileEntity? file { get; set; }

    public string fullName()
    {
        return name + " " + secondName + " " + surname + " " + secondSurname;
    }

    public void init()
    {
        isActive = true;
        schoolYear = "";
    }
    
    public void updateData(StudentEntity entity)
    {
        name = entity.name;
        secondName = entity.secondName;
        surname = entity.surname;
        secondSurname = entity.secondSurname;
        isActive = entity.isActive;
        sex = entity.sex;
        birthday = entity.birthday;
        religion = entity.religion;
        diseases = entity.diseases;
    }

    public void update(StudentEntity entity)
    {
        throw new NotImplementedException();
    }
    
    public class Builder
    {
        private readonly StudentEntity entity;

        public Builder()
        {
            entity = new StudentEntity();
            
            entity.parents = [];
            entity.file = new StudentFileEntity();
            entity.tutor = new StudentTutorEntity();
            entity.measurements = new StudentMeasurementsEntity();

            entity.schoolYear = "";
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

        public Builder setDiseases(string diseases)
        {
            entity.diseases = diseases;
            return this;
        }

        public Builder setFile(StudentFileEntity file)
        {
            entity.file = file;
            entity.file.fileId = "1223";
            return this;
        }

        public Builder setTutor(StudentTutorEntity tutor)
        {
            entity.tutor = tutor;
            entity.tutor.tutorId = "asdf";
            return this;
        }

        public Builder setMeasurements(StudentMeasurementsEntity studentMeasurements)
        {
            entity.measurements = studentMeasurements;
            entity.measurements.measurementId = "asasfa";
            return this;
        }

        public Builder setParents(List<StudentParentEntity> parents)
        {
            foreach (var item in parents)
            {
                item.parentId = item.idCard;
            }
            
            entity.parents = parents;
            return this;
        }
    }
}