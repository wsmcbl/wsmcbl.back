namespace wsmcbl.src.model.config;

public class UserEntity
{
    public Guid? userId { get; set; }
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string password { get; set; } = null!;
    public string email { get; set; } = null!;
    public bool isActive { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public RoleEntity role { get; set; }
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
        return role.name;
    }
    
    public List<string> getPermissionList()
    {
        return permissionList.Select(e => e.name).ToList();
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

        public Builder setEmail(string email)
        {
            entity.email = email;
            return this;
        }

        public Builder setPassword(string password)
        {
            entity.password = password;
            return this;
        }

        public Builder setRole(int roleId)
        {
            entity.roleId = roleId;
            return this;
        }
    }
}