using wsmcbl.src.model.dao;
using wsmcbl.src.utilities;

namespace wsmcbl.src.model.config;

public class UserEntity
{
    public Guid? userId { get; set; }
    public int roleId { get; set; }
    public string? userRoleId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string password { get; set; } = null!;
    public string email { get; set; } = null!;
    public bool isActive { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public RoleEntity? role { get; set; }
    public List<PermissionEntity> permissionList { get; set; } = [];
    
    public string fullName()
    {
        return $"{name} {secondName} {surname} {secondSurname}";
    }
    
    private void markAsUpdated()
    {
        updatedAt = DateTime.UtcNow;
    }

    public string getRole()
    {
        return role!.name;
    }
    
    public List<string> getPermissionList()
    {
        return permissionList.Select(e => e.name).ToList();
    }
    
    public string getAlias()
    {
        return $"{name[0]}. {surname}";
    }

    public void update(UserEntity user)
    {
        roleId = user.roleId;
        name = user.name.Trim();
        secondName = user.secondName?.Trim();
        surname = user.surname.Trim();
        secondSurname = user.secondSurname?.Trim();
        email = user.email.Trim();
        isActive = user.isActive;
        markAsUpdated();
    }

    public async Task generateEmail(IUserDao userDao)
    {
        var name_email = getTextInEmailFormat(name);
        var surname_email = getTextInEmailFormat(surname);
        var random = new Random();
        
        while (true)
        {
            email = $"{name_email}.{surname_email}{random.Next(10, 99)}@cbl-edu.com";
        
            var isDuplicate = await userDao.isEmailDuplicate(email);
            if (!isDuplicate)
            {
                break;
            }
        }
    }

    private static string getTextInEmailFormat(string value)
    {
        return value.Trim().ToLower().convertToEmailFormat();
    }


    public async Task getIdFromRole(DaoFactory daoFactory)
    {
        userRoleId = string.Empty;
        if (roleId is 1 or 4)
        {
            var result = await daoFactory.teacherDao!.getByUserId((Guid)userId!);
            userRoleId = result.teacherId;
        }
    }

    public class Builder
    {
        private readonly UserEntity entity;

        public Builder()
        {
            entity = new UserEntity()
            {
                userId = null,
                createdAt = DateTime.UtcNow,
                isActive = true
            };
            entity.markAsUpdated();
        }

        public UserEntity build() => entity;

        public Builder setName(string name)
        {
            entity.name = name.Trim();
            return this;
        }

        public Builder setSecondName(string? secondName)
        {
            entity.secondName = secondName?.Trim();
            return this;
        }

        public Builder setSurname(string surname)
        {
            entity.surname = surname.Trim();
            return this;
        }

        public Builder setSecondSurname(string? secondSurname)
        {
            entity.secondSurname = secondSurname?.Trim();
            return this;
        }

        public Builder setRole(int roleId)
        {
            entity.roleId = roleId;
            return this;
        }
    }
}