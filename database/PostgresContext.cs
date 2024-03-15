using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;

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
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "accounting");

            entity.Property(e => e.tutor).HasColumnName("tutor");
            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasColumnName("studentid");
            entity.Property(e => e.name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.secondName)
                .HasMaxLength(50)
                .HasColumnName("secondname");
            entity.Property(e => e.secondSurname)
                .HasMaxLength(50)
                .HasColumnName("secondsurname");
            entity.Property(e => e.schoolYear).HasColumnName("schoolyear");
            entity.Property(e => e.surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.enrollment)
                .HasMaxLength(20)
                .HasColumnName("enrollmentid");
        });
    }
}
