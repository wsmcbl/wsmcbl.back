using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.secretary;

public class StudentEntity
{
    public string? studentId { get; set; }
    public string tutorId { get; set; } = null!;
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


    public StudentFileEntity? file { get; set; }
    public StudentTutorEntity? tutor { get; set; }
    public List<StudentParentEntity>? parents { get; set; }
    public StudentMeasurementsEntity? measurements { get; set; }

    public string fullName()
    {
        return $"{name} {secondName} {surname} {secondSurname}";
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
        address = entity.address;
    }

    public async Task saveChanges(DaoFactory daoFactory)
    {
        await daoFactory.studentDao!.updateAsync(this);
        await daoFactory.studentTutorDao!.updateAsync(tutor);
        await daoFactory.studentFileDao!.updateAsync(file);
        await daoFactory.studentMeasurementsDao!.updateAsync(measurements);

        foreach (var parent in parents!)
        {
            await daoFactory.studentParentDao!.updateAsync(parent);
        }

        await daoFactory.execute();
    }

    public string getStringData()
    {
        return $"{fullName()} - {sex} - {birthday.ToString()}";
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
                measurements = new StudentMeasurementsEntity()
            };
        }

        public StudentEntity build() => entity;

        public Builder setId(string studentId)
        {
            entity.studentId = studentId;
            return this;
        }

        public Builder setTutorId(string tutorId)
        {
            entity.tutorId = tutorId;
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

        public Builder isActive(bool isActive = true)
        {
            entity.isActive = isActive;
            return this;
        }

        public Builder setSex(bool sex = true)
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

        public Builder setFile(StudentFileEntity? file)
        {
            if (file != null)
            {
                file.studentId = entity.studentId!;
            }

            entity.file = file;
            return this;
        }

        public Builder setTutor(StudentTutorEntity tutor)
        {
            entity.tutor = tutor;
            return this;
        }

        public Builder setMeasurements(StudentMeasurementsEntity? studentMeasurements)
        {
            if (studentMeasurements != null)
            {
                studentMeasurements.studentId = entity.studentId!;
            }

            entity.measurements = studentMeasurements;
            return this;
        }

        public Builder setParents(List<StudentParentEntity> parents)
        {
            if (parents.Count != 0)
            {
                foreach (var item in parents)
                {
                    item.studentId = entity.studentId!;
                }
            }

            entity.parents = parents;
            return this;
        }

        public Builder setAddress(string address)
        {
            entity.address = address;
            return this;
        }
    }
}