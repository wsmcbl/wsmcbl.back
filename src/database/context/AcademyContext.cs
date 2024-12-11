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

            entity.Property(e => e.teacherId).HasMaxLength(20).HasColumnName("teacherid");
            entity.Property(e => e.capacity).HasColumnName("capacity");
            entity.Property(e => e.label).HasMaxLength(20).HasColumnName("label");
            entity.Property(e => e.tag).HasColumnName("tag");
            entity.Property(e => e.degreeId).HasColumnName("degreeid");
            entity.Property(e => e.quantity).HasColumnName("quantity");
            entity.Property(e => e.schoolYear).HasMaxLength(20).HasColumnName("schoolyear");
            entity.Property(e => e.section).HasMaxLength(10).HasColumnName("section");

            entity.HasMany(d => d.studentList).WithOne()
                .HasForeignKey(d => d.enrollmentId);

            entity.HasMany(d => d.subjectList).WithOne()
                .HasForeignKey(d => d.enrollmentId);
        });

        modelBuilder.Entity<GradeEntity>(entity =>
        {
            entity.HasKey(e => e.gradeId).HasName("grade_pkey");

            entity.ToTable("grade", "academy");

            entity.Property(e => e.gradeId).HasColumnName("gradeid");
            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
            entity.Property(e => e.subjectPartialId).HasColumnName("subjectpartialid");
            entity.Property(e => e.grade).HasColumnName("grade");
            entity.Property(e => e.conductGrade).HasColumnName("conductgrade");
            entity.Property(e => e.label).HasColumnName("label");
        });
        
        modelBuilder.Entity<PartialEntity>(entity =>
        {
            entity.HasKey(e => e.partialId).HasName("partial_pkey");

            entity.ToTable("partial", "academy");

            entity.Property(e => e.partialId).HasColumnName("partialid");
            entity.Property(e => e.semesterId).HasColumnName("semesterid");
            entity.Property(e => e.partial).HasColumnName("partial");
            entity.Property(e => e.label).HasMaxLength(20).HasColumnName("label");
            entity.Property(e => e.deadLine).HasColumnName("deadline");
            entity.Property(e => e.startDate).HasColumnName("startdate");
            entity.Property(e => e.gradeRecordIsActive).HasColumnName("graderecordisactive");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            
            entity.HasMany(d => d.subjectPartialList)
                .WithOne()
                .HasForeignKey(e => e.partialId);
            entity.Ignore(e => e.semester);
        });

        modelBuilder.Entity<SemesterEntity>(entity =>
        {
            entity.HasKey(e => e.semesterId).HasName("semester_pkey");

            entity.ToTable("semester", "academy");

            entity.Property(e => e.semesterId).HasColumnName("semesterid");
            entity.Property(e => e.schoolyear).HasMaxLength(20).HasColumnName("schoolyear");
            entity.Property(e => e.deadLine).HasColumnName("deadline");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            entity.Property(e => e.label).HasMaxLength(20).HasColumnName("label");
            entity.Property(e => e.semester).HasColumnName("semester");

            entity.HasMany(d => d.partialList)
                .WithOne()
                .HasForeignKey(e => e.semesterId);
        });

        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => new { Studentid = e.studentId, Enrollmentid = e.enrollmentId }).HasName("student_pkey");

            entity.ToTable("student", "academy");

            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
            entity.Property(e => e.enrollmentId).HasMaxLength(15).HasColumnName("enrollmentid");
            entity.Property(e => e.isApproved).HasColumnName("isapproved");
            entity.Property(e => e.isRepeating).HasColumnName("isrepeating");
            entity.Property(e => e.createdAt).HasColumnName("createdat");
            entity.Property(e => e.schoolYear).HasMaxLength(20).HasColumnName("schoolyear");

            entity.HasOne(d => d.student).WithMany()
                .HasForeignKey(d => d.studentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_studentid_fkey");

            entity.Ignore(e => e.partials);
            entity.Ignore(e => e.enrollmentLabel);
        });

        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => new { Subjectid = e.subjectId, Enrollmentid = e.enrollmentId }).HasName("subject_pkey");

            entity.ToTable("subject", "academy");

            entity.Property(e => e.subjectId).HasMaxLength(15).HasColumnName("subjectid");
            entity.Property(e => e.enrollmentId).HasMaxLength(15).HasColumnName("enrollmentid");
            entity.Property(e => e.teacherId).HasMaxLength(15).HasColumnName("teacherid");

            entity.HasOne(e => e.secretarySubject)
                .WithMany()
                .HasForeignKey(e => e.subjectId);
        });

        modelBuilder.Entity<SubjectPartialEntity>(entity =>
        {
            entity.HasKey(e => e.subjectPartialId).HasName("subject_partial_key");

            entity.ToTable("subject_partial", "academy");
            
            entity.Property(e => e.subjectPartialId).HasColumnName("subjectpartialid");
            entity.Property(e => e.partialId).HasColumnName("partialid");
            entity.Property(e => e.teacherId).HasMaxLength(15).HasColumnName("teacherid");
            entity.Property(e => e.subjectId).HasMaxLength(15).HasColumnName("subjectid");
            entity.Property(e => e.enrollmentId).HasMaxLength(15).HasColumnName("enrollmentid");
            
            entity.HasMany(d => d.gradeList).WithOne()
                .HasForeignKey(d => d.subjectPartialId);

            entity.Ignore(e => e.studentGrade);
        });

        modelBuilder.Entity<TeacherEntity>(entity =>
        {
            entity.HasKey(e => e.teacherId).HasName("teacher_pkey");

            entity.ToTable("teacher", "academy");

            entity.Property(e => e.teacherId).HasMaxLength(15).HasColumnName("teacherid");
            entity.Property(e => e.userId).HasMaxLength(15).HasColumnName("userid");
            entity.Property(e => e.isGuide).HasColumnName("isguide");

            entity.HasOne(d => d.user).WithMany()
                .HasForeignKey(d => d.userId).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_userid_fkey");
            
            entity.Ignore(e => e.enrollment);
        });
    }
}