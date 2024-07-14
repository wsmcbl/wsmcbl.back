using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database.context;

internal class SecretaryContext
{
    private readonly ModelBuilder modelBuilder;

    public SecretaryContext(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void create()
    {
        modelBuilder.Entity<GradeEntity>(entity =>
        {
            entity.HasKey(e => e.gradeId).HasName("grade_pkey");

            entity.ToTable("grade", "secretary");

            entity.Property(e => e.gradeId).HasColumnName("gradeid");
            entity.Property(e => e.label)
                .HasMaxLength(25)
                .HasColumnName("gradelabel");
            entity.Property(e => e.schoolYear)
                .HasMaxLength(15)
                .HasColumnName("schoolyear");
        });

        modelBuilder.Entity<SchoolyearEntity>(entity =>
        {
            entity.HasKey(e => e.schoolYearId).HasName("schoolyear_pkey");

            entity.ToTable("schoolyear", "secretary");

            entity.Property(e => e.schoolYearId)
                .HasMaxLength(15)
                .HasColumnName("schoolyearid");
            entity.Property(e => e.deadLine).HasColumnName("deadline");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            entity.Property(e => e.label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.startDate).HasColumnName("startdate");

            entity.HasMany(d => d.students).WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "SchoolyearStudent",
                    r => r.HasOne<StudentEntity>().WithMany()
                        .HasForeignKey("Studentid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("schoolyear_student_studentid_fkey"),
                    l => l.HasOne<SchoolyearEntity>().WithMany()
                        .HasForeignKey("Schoolyear")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("schoolyear_student_schoolyear_fkey"),
                    j =>
                    {
                        j.HasKey("Schoolyear", "Studentid").HasName("schoolyear_student_pkey");
                        j.ToTable("schoolyear_student", "secretary");
                        j.IndexerProperty<string>("Schoolyear")
                            .HasMaxLength(15)
                            .HasColumnName("schoolyear");
                        j.IndexerProperty<string>("Studentid")
                            .HasMaxLength(20)
                            .HasColumnName("studentid");
                    });
        });
        
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "secretary");

            entity.Property(e => e.tutor)
                .HasColumnName("tutor");
            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasDefaultValueSql("secretary.generate_student_id()")
                .ValueGeneratedOnAdd()
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
            entity.Property(e => e.surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.schoolYear)
                .HasColumnName("schoolyear");
            entity.Property(e => e.enrollmentLabel)
                .HasMaxLength(20)
                .HasColumnName("enrollmentlabel");
            entity.Property(e => e.birthday)
                .HasColumnName("birthday");
            entity.Property(e => e.sex)
                .HasColumnName("sex");
            entity.Property(e => e.isActive)
                .HasColumnName("studentstate");
        });
        
        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.subjectId).HasName("subject_pkey");

            entity.ToTable("subject", "secretary");

            entity.Property(e => e.subjectId)
                .HasMaxLength(15)
                .HasColumnName("subjectid");
            entity.Property(e => e.gradeId).HasColumnName("gradeid");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });
    }
}