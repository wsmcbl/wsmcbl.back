using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database.context;

public class ConfigContext
{
    private readonly ModelBuilder modelBuilder;

    public ConfigContext(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void create()
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.userId).HasName("user_pkey");

            entity.ToTable("user", "config");

            entity.Property(e => e.userId).HasMaxLength(100).ValueGeneratedOnAdd().HasColumnName("userid");
            entity.Property(e => e.roleId).HasColumnName("roleid");
            entity.Property(e => e.email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.password).HasMaxLength(100).HasColumnName("password");
            entity.Property(e => e.secondName).HasMaxLength(50).HasColumnName("secondname");
            entity.Property(e => e.secondSurname).HasMaxLength(50).HasColumnName("secondsurname");
            entity.Property(e => e.surname).HasMaxLength(50).HasColumnName("surname");
            entity.Property(e => e.isActive).HasColumnName("userstate");
            entity.Property(e => e.createdAt).HasColumnName("createdat");
            entity.Property(e => e.updatedAt).HasColumnName("updatedat");

            entity.HasOne(e => e.role).WithMany().HasForeignKey(e => e.roleId);

            entity.HasMany(r => r.permissionList)
                .WithMany()
                .UsingEntity<UserPermission>("user_permission",
                    l => l.HasOne<PermissionEntity>().WithMany()
                        .HasForeignKey(e => e.permissionid),
                    r => r.HasOne<UserEntity>().WithMany()
                        .HasForeignKey(e => e.userid));
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.roleId).HasName("role_pkey");

            entity.ToTable("role", "config");

            entity.Property(e => e.roleId).HasColumnName("roleid");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.description).HasMaxLength(150).HasColumnName("description");

            entity.HasMany(e => e.permissionList)
                .WithMany();
        });


        modelBuilder.Entity<PermissionEntity>(entity =>
        {
            entity.HasKey(e => e.permissionId).HasName("permission_pkey");

            entity.ToTable("permission", "config");

            entity.Property(e => e.permissionId).HasColumnName("permissionid");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.description).HasMaxLength(150).HasColumnName("description");
        });
    }
    
    private class UserPermission
    {
        public Guid userid { get; set; }
        public int permissionid { get; set; }
    }
}