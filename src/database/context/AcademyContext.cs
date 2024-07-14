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
            entity.HasKey(e => e.Enrollmentid).HasName("enrollment_pkey");

            entity.ToTable("enrollment", "academy");

            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Enrollmentlabel)
                .HasMaxLength(20)
                .HasColumnName("enrollmentlabel");
            entity.Property(e => e.Gradeid).HasColumnName("gradeid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Schoolyear)
                .HasMaxLength(20)
                .HasColumnName("schoolyear");
            entity.Property(e => e.Section)
                .HasMaxLength(2)
                .HasColumnName("section");

            entity.HasOne(d => d.Grade).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Gradeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_gradeid_fkey");

            entity.HasOne(d => d.SchoolyearNavigation).WithMany()
                .HasForeignKey(d => d.Schoolyear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_schoolyear_fkey");
        });
        
        modelBuilder.Entity<NoteEntity>(entity =>
        {
            entity.HasKey(e => new { e.Studentid, e.Subjectid }).HasName("note_pkey");

            entity.ToTable("note", "academy");

            entity.Property(e => e.Studentid)
                .HasMaxLength(15)
                .HasColumnName("studentid");
            entity.Property(e => e.Subjectid)
                .HasMaxLength(15)
                .HasColumnName("subjectid");
            entity.Property(e => e.Cumulative).HasColumnName("cumulative");
            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Exam).HasColumnName("exam");
            entity.Property(e => e.Finalscore).HasColumnName("finalscore");

            entity.HasOne(d => d.Subject).WithMany(p => p.Notes)
                .HasForeignKey(d => d.Subjectid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("note_subjectid_fkey");

            entity.HasOne(d => d.Student2).WithMany(p => p.Notes)
                .HasForeignKey(d => new { d.Studentid, d.Enrollmentid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("note_studentid_enrollmentid_fkey");
        });
        
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => new { e.Studentid, e.Enrollmentid }).HasName("student_pkey");

            entity.ToTable("student", "academy");

            entity.Property(e => e.Studentid)
                .HasMaxLength(15)
                .HasColumnName("studentid");
            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Isapproved).HasColumnName("isapproved");
            entity.Property(e => e.Schoolyear)
                .HasMaxLength(20)
                .HasColumnName("schoolyear");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.Student2s)
                .HasForeignKey(d => d.Enrollmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_enrollmentid_fkey");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_studentid_fkey");
        });
        
        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.Subjectid).HasName("subject_pkey");

            entity.ToTable("subject", "academy");

            entity.Property(e => e.Subjectid)
                .HasMaxLength(15)
                .HasColumnName("subjectid");
            entity.Property(e => e.Basesubjectid)
                .HasMaxLength(15)
                .HasColumnName("basesubjectid");
            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(15)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Teacherid)
                .HasMaxLength(15)
                .HasColumnName("teacherid");

            entity.HasOne(d => d.Basesubject).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.Basesubjectid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_basesubjectid_fkey");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.Enrollmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_enrollmentid_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_teacherid_fkey");
        });
        
        modelBuilder.Entity<TeacherEntity>(entity =>
        {
            entity.HasKey(e => e.Teacherid).HasName("teacher_pkey");

            entity.ToTable("teacher", "academy");

            entity.Property(e => e.Teacherid)
                .HasMaxLength(15)
                .HasColumnName("teacherid");
            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(20)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Userid)
                .HasMaxLength(15)
                .HasColumnName("userid");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.Enrollmentid)
                .HasConstraintName("teacher_enrollmentid_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_userid_fkey");
        });

    }
}