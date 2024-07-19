using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database.context;

internal class AcademyContext
{
    private readonly ModelBuilder modelBuilder;

    public AcademyContext(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void create()
    {
        modelBuilder.Entity<EnrollmentEntity>(entity =>
        {
            entity.HasKey(e => e.enrollmentId).HasName("enrollment_pkey");

            entity.ToTable("enrollment", "academy");

            entity.Property(e => e.enrollmentId).HasMaxLength(15)
                .HasDefaultValueSql("academy.generate_enrollment_id()")
                .HasColumnName("enrollmentid");
            
            entity.Property(e => e.capacity).HasColumnName("capacity");
            entity.Property(e => e.label).HasMaxLength(20).HasColumnName("enrollmentlabel");
            entity.Property(e => e.gradeId).HasColumnName("gradeid");
            entity.Property(e => e.quantity).HasColumnName("quantity");
            entity.Property(e => e.schoolYear).HasMaxLength(20).HasColumnName("schoolyear");
            entity.Property(e => e.section).HasMaxLength(10).HasColumnName("section");

            entity.HasMany(d => d.studentList).WithOne()
                .HasForeignKey(d => d.enrollmentId);
            
            entity.HasMany(d => d.subjectList).WithOne()
                .HasForeignKey(d => d.enrollmentId);
        });
        
        modelBuilder.Entity<ScoreEntity>(entity =>
        {
            entity.HasKey(e => new { Studentid = e.studentId, Subjectid = e.subjectId }).HasName("note_pkey");

            entity.ToTable("note", "academy");

            entity.Property(e => e.studentId)
                .HasMaxLength(15)
                .HasColumnName("studentid");
            entity.Property(e => e.subjectId)
                .HasMaxLength(15)
                .HasColumnName("subjectid");
            entity.Property(e => e.cumulative).HasColumnName("cumulative");
            entity.Property(e => e.enrollmentId)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.exam).HasColumnName("exam");
            entity.Property(e => e.finalScore).HasColumnName("finalscore");
        });
        
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => new { Studentid = e.studentId, Enrollmentid = e.enrollmentId }).HasName("student_pkey");

            entity.ToTable("student", "academy");

            entity.Property(e => e.studentId)
                .HasMaxLength(15)
                .HasColumnName("studentid");
            entity.Property(e => e.enrollmentId)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.isApproved).HasColumnName("isapproved");
            entity.Property(e => e.schoolYear)
                .HasMaxLength(20)
                .HasColumnName("schoolyear");

            entity.HasOne(d => d.student).WithMany()
                .HasForeignKey(d => d.studentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_studentid_fkey");

            entity.HasMany(d => d.scores)
                .WithOne()
                .HasForeignKey(e => new { e.studentId, e.enrollmentId });
        });
        
        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.subjectId).HasName("subject_pkey");

            entity.ToTable("subject", "academy");

            entity.Property(e => e.subjectId).HasMaxLength(15).HasColumnName("subjectid");
            entity.Property(e => e.enrollmentId).HasMaxLength(15).HasColumnName("enrollmentid");
            entity.Property(e => e.teacherId).HasMaxLength(15).HasColumnName("teacherid");

            entity.HasOne(e => e.secretarySubject).WithMany().HasForeignKey(e => e.subjectId);
            
            entity.HasMany(d => d.scores).WithOne().HasForeignKey(e => e.subjectId);
        });
        
        modelBuilder.Entity<TeacherEntity>(entity =>
        {
            entity.HasKey(e => e.teacherId).HasName("teacher_pkey");

            entity.ToTable("teacher", "academy");

            entity.Property(e => e.teacherId)
                .HasMaxLength(15)
                .HasColumnName("teacherid");
            entity.Property(e => e.enrollmentId)
                .HasMaxLength(20)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.userId)
                .HasMaxLength(15)
                .HasColumnName("userid");
            entity.Property(e => e.isGuide).HasColumnName("isguide");

            entity.HasOne(d => d.enrollment)
                .WithMany()
                .HasForeignKey(d => d.enrollmentId)
                .HasConstraintName("teacher_enrollmentid_fkey");

            entity.HasOne(d => d.user)
                .WithMany()
                .HasForeignKey(d => d.userId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_userid_fkey");

            entity.HasMany(d => d.subjects)
                .WithOne()
                .HasForeignKey(d => d.teacherId);
        });

    }
}