using System.Security.Cryptography;
using System.Text;
using wsmcbl.src.utilities;

namespace wsmcbl.src.model.secretary;

public class StudentEntity
{
    public string? studentId { get; set; }
    public string tutorId { get; set; } = null!;
    public string? minedId { get; set; }
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
    public string? accessToken { get; set; }
    public byte[]? profilePicture { get; set; }


    public StudentFileEntity? file { get; set; }
    public StudentTutorEntity tutor { get; set; } = null!;
    public List<StudentParentEntity>? parents { get; set; }
    public StudentMeasurementsEntity? measurements { get; set; }
    
    public string fullName()
    {
        var builder = new StringBuilder(name);
        builder.AppendName(secondName);
        builder.Append(' ').Append(surname);
        builder.AppendName(secondSurname);
        
        return builder.ToString();
    }

    public string getTutorName()
    {
        return tutor.name;
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
        minedId = entity.minedId;
    }

    public string getStringData()
    {
        return $"{fullName()} - {sex} - {birthday.ToString()}";
    }
    
    public void generateAccessToken()
    {
        accessToken = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
    }

    public int getAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthday.Year;
        
        if (today < new DateOnly(today.Year, birthday.Month, birthday.Day))
        {
            age--;
        }

        return age;
    }

    public void changeState()
    {
        isActive = !isActive;
    }
    
    public class Builder
    {
        private readonly StudentEntity entity = new();

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
            if (file == null)
            {
                return this;
            }

            file.studentId = entity.studentId!;
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
            if (studentMeasurements == null)
            {
                return this;
            }

            studentMeasurements.studentId = entity.studentId!;
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

        public Builder setMinedId(string? minedId)
        {
            entity.minedId = minedId;
            return this;
        }
    }
}