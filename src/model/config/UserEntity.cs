namespace wsmcbl.src.model.config;

public class UserEntity
{
    public string userId { get; set; } = null!;

    public string name { get; set; } = null!;

    public string? secondName { get; set; }

    public string surname { get; set; } = null!;

    public string? secondsurName { get; set; }

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    public string? email { get; set; }

    public bool isActive { get; set; }
    
    public string fullName() 
    {
        return name + " " + secondName + " " + surname + " " + secondsurName;
    }
}