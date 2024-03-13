using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.entity.academy;

namespace wsmcbl.back.database;

public partial class PostgresContext : DbContext
{
    public virtual DbSet<StudentEntity> Student { get; set; } = null!;
    
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("student");

            entity.Property(e => e.birthday).HasColumnName("birthday");
            entity.Property(e => e.id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.secondName)
                .HasMaxLength(100)
                .HasColumnName("secondname");
            entity.Property(e => e.secondSurname)
                .HasMaxLength(100)
                .HasColumnName("secondsurname");
            entity.Property(e => e.sex).HasColumnName("sex");
            entity.Property(e => e.surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });
    }
}
