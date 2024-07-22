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

            entity.Property(e => e.gradeId)
                .HasMaxLength(25)
                .HasDefaultValueSql("secretary.generate_grade_id()")
                .HasColumnName("gradeid");
            entity.Property(e => e.label).HasMaxLength(25).HasColumnName("gradelabel");
            entity.Property(e => e.schoolYear).HasMaxLength(15).HasColumnName("schoolyear");
            entity.Property(e => e.quantity).HasColumnName("quantity");
            entity.Property(e => e.modality).HasMaxLength(50).HasColumnName("modality");

            entity.HasMany(e => e.enrollments).WithOne()
                .HasForeignKey(d => d.gradeId);

            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.gradeId);
        });

        modelBuilder.Entity<SchoolYearEntity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("schoolyear_pkey");

            entity.ToTable("schoolyear", "secretary");

            entity.Ignore(e => e.gradeList);
            entity.Ignore(e => e.tariffList);

            entity.Property(e => e.id)
                .HasMaxLength(15)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("secretary.generate_schoolyear_id()")
                .HasColumnName("schoolyearid");
            
            entity.Property(e => e.deadLine).HasColumnName("deadline");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            entity.Property(e => e.label).HasMaxLength(100).HasColumnName("label");
            entity.Property(e => e.startDate).HasColumnName("startdate");
        });

        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "secretary");

            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasDefaultValueSql("secretary.generate_student_id()")
                .ValueGeneratedOnAdd()
                .HasColumnName("studentid");

            entity.Property(e => e.tutor).HasColumnName("tutor");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.secondName).HasMaxLength(50).HasColumnName("secondname");
            entity.Property(e => e.secondSurname).HasMaxLength(50).HasColumnName("secondsurname");
            entity.Property(e => e.surname).HasMaxLength(50).HasColumnName("surname");
            entity.Property(e => e.schoolYear).HasColumnName("schoolyear");
            entity.Property(e => e.birthday).HasColumnName("birthday");
            entity.Property(e => e.sex).HasColumnName("sex");
            entity.Property(e => e.isActive).HasColumnName("studentstate");
        });

        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.subjectId).HasName("subject_pkey");

            entity.ToTable("subject", "secretary");

            entity.Property(e => e.subjectId)
                .HasMaxLength(15)
                .HasDefaultValueSql("secretary.generate_subject_id()")
                .HasColumnName("subjectid");
            
            entity.Property(e => e.gradeId).HasColumnName("gradeid");
            entity.Property(e => e.name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
        });

        modelBuilder.Entity<GradeDataEntity>(entity =>
        {
            entity.HasKey(e => e.gradeDataId).HasName("gradecatalog_pkey");

            entity.ToTable("gradecatalog", "secretary");

            entity.Property(e => e.gradeDataId).HasColumnName("gradecatalogid");
            entity.Property(e => e.label).HasMaxLength(50).HasColumnName("gradelabel");
            entity.Property(e => e.modality).HasColumnName("modality");
            
            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.gradeDataId);
        });

        modelBuilder.Entity<SubjectDataEntity>(entity =>
        {
            entity.HasKey(e => e.subjectDataId).HasName("subjectcatalog_pkey");

            entity.ToTable("subjectcatalog", "secretary");

            entity.Property(e => e.subjectDataId).HasColumnName("subjectcatalogid");
            entity.Property(e => e.gradeDataId).HasColumnName("gradecatalogid");
            entity.Property(e => e.name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
        });

        modelBuilder.Entity<TariffDataEntity>(entity =>
        {
            entity.HasKey(e => e.tariffDataId).HasName("tariffcatalog_pkey");

            entity.ToTable("tariffcatalog", "secretary");

            entity.Property(e => e.tariffDataId).HasColumnName("tariffcatalogid");
            entity.Property(e => e.amount).HasColumnName("amount");
            entity.Property(e => e.concept).HasMaxLength(100).HasColumnName("concept");
            entity.Property(e => e.dueDate).HasColumnName("duedate");
            entity.Property(e => e.modality).HasColumnName("modality");
            entity.Property(e => e.typeId).HasColumnName("typeid");
        });

        modelBuilder.Entity<StudentFileEntity>(entity =>
        {
            entity.HasKey(e => e.fileId);

            entity.ToTable("studentfile", "secretary");

            entity.Property(e => e.fileId).HasColumnName("fileid");
        });

        modelBuilder.Entity<StudentContactEntity>(entity =>
        {
            entity.HasKey(e => e.contactId);

            entity.ToTable("studentcontact", "secretary");

            entity.Property(e => e.contactId).HasColumnName("contactid");
        });

        modelBuilder.Entity<StudentMeasurementsEntity>(entity =>
        {
            entity.HasKey(e => e.measurementId);

            entity.ToTable("studentmeasurements", "secretary");

            entity.Property(e => e.measurementId).HasColumnName("measurementid");
        });
    }
}