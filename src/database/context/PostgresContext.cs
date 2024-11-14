using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database.context;

public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var academy = new AcademyContext(modelBuilder);
        var accounting = new AccountingContext(modelBuilder);
        var secretary = new SecretaryContext(modelBuilder);
        
        academy.create();
        accounting.create();
        secretary.create();
        
        
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.userId).HasName("user_pkey");

            entity.ToTable("user", "config");

            entity.Property(e => e.userId).HasMaxLength(100).ValueGeneratedOnAdd().HasColumnName("userid");
            entity.Property(e => e.email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.password).HasMaxLength(100).HasColumnName("password");
            entity.Property(e => e.secondName).HasMaxLength(50).HasColumnName("secondname");
            entity.Property(e => e.secondSurname).HasMaxLength(50).HasColumnName("secondsurname");
            entity.Property(e => e.surname).HasMaxLength(50).HasColumnName("surname");
            entity.Property(e => e.isActive).HasColumnName("userstate");
            entity.Property(e => e.createdAt).HasColumnName("createdat");
            entity.Property(e => e.updatedAt).HasColumnName("updatedat");
        });
        
        modelBuilder.HasSequence("enrollment_id_seq", "academy").StartsAt(10L);
        modelBuilder.HasSequence("grade_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("schoolyear_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("student_id_seq", "secretary");
        modelBuilder.HasSequence("subject_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("transaction_id_seq", "accounting").StartsAt(10L);
    }
}