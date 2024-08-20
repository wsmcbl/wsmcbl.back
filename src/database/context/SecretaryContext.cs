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
        modelBuilder.Entity<DegreeEntity>(entity =>
        {
            entity.HasKey(e => e.degreeId).HasName("degree_pkey");

            entity.ToTable("degree", "secretary");

            entity.Property(e => e.degreeId)
                .HasMaxLength(25)
                .HasDefaultValueSql("secretary.generate_degree_id()")
                .HasColumnName("degreeid");
            entity.Property(e => e.label).HasMaxLength(25).HasColumnName("label");
            entity.Property(e => e.schoolYear).HasMaxLength(15).HasColumnName("schoolyear");
            entity.Property(e => e.quantity).HasColumnName("quantity");
            entity.Property(e => e.modality).HasMaxLength(50).HasColumnName("modality");

            entity.HasMany(e => e.enrollments).WithOne()
                .HasForeignKey(d => d.degreeId);

            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.degreeId);
        });

        modelBuilder.Entity<SchoolYearEntity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("schoolyear_pkey");

            entity.ToTable("schoolyear", "secretary");

            entity.Ignore(e => e.degreeList);
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

            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.secondName).HasMaxLength(50).HasColumnName("secondname");
            entity.Property(e => e.secondSurname).HasMaxLength(50).HasColumnName("secondsurname");
            entity.Property(e => e.surname).HasMaxLength(50).HasColumnName("surname");
            entity.Property(e => e.schoolYear).HasColumnName("schoolyear");
            entity.Property(e => e.birthday).HasColumnName("birthday");
            entity.Property(e => e.sex).HasColumnName("sex");
            entity.Property(e => e.isActive).HasColumnName("studentstate");
            entity.Property(e => e.diseases).HasColumnName("diseases");
            entity.Property(e => e.religion).HasColumnName("religion");
            entity.Property(e => e.address).HasMaxLength(100).HasColumnName("address");

            entity.Ignore(e => e.file);
            entity.Ignore(e => e.tutor);
            entity.Ignore(e => e.parents);
            entity.Ignore(e => e.measurements);
        });

        modelBuilder.Entity<StudentFileEntity>(entity =>
        {
            entity.HasKey(e => e.fileId).HasName("studentfile_pkey");

            entity.ToTable("studentfile", "secretary");

            entity.Property(e => e.fileId).HasColumnName("fileid");
            
            entity.Property(e => e.birthDocument).HasColumnName("birthdocument");
            entity.Property(e => e.conductDocument).HasColumnName("conductdocument");
            entity.Property(e => e.financialSolvency).HasColumnName("financialsolvency");
            entity.Property(e => e.parentIdentifier).HasColumnName("parentidentifier");
            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
            entity.Property(e => e.transferSheet).HasColumnName("transfersheet");
            entity.Property(e => e.updatedDegreeReport).HasColumnName("updatedgradereport");
        });

        modelBuilder.Entity<StudentMeasurementsEntity>(entity =>
        {
            entity.HasKey(e => e.measurementId).HasName("studentmeasurements_pkey");

            entity.ToTable("studentmeasurements", "secretary");

            entity.Property(e => e.measurementId).HasColumnName("measurementid");
            entity.Property(e => e.height).HasColumnName("height");
            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
            entity.Property(e => e.weight).HasColumnName("weight");
        });

        modelBuilder.Entity<StudentParentEntity>(entity =>
        {
            entity.HasKey(e => e.parentId).HasName("studentparent_pkey");

            entity.ToTable("studentparent", "secretary");

            entity.Property(e => e.parentId)
                .HasMaxLength(15)
                .HasDefaultValueSql("secretary.generate_parent_id()")
                .HasColumnName("parentid");
            
            entity.Property(e => e.idCard).HasMaxLength(25).HasColumnName("idcard");
            entity.Property(e => e.name).HasMaxLength(70).HasColumnName("name");
            entity.Property(e => e.occupation).HasMaxLength(30).HasColumnName("occupation");
            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
            entity.Property(e => e.sex).HasColumnName("sex");
        });

        modelBuilder.Entity<StudentTutorEntity>(entity =>
        {
            entity.HasKey(e => e.tutorId).HasName("studenttutor_pkey");

            entity.ToTable("studenttutor", "secretary");

            entity.Property(e => e.tutorId)
                .HasMaxLength(15)
                .HasDefaultValueSql("secretary.generate_tutor_id()")
                .HasColumnName("tutorid");
            
            entity.Property(e => e.name).HasMaxLength(70).HasColumnName("name");
            entity.Property(e => e.phone).HasMaxLength(12).HasColumnName("phone");
            entity.Property(e => e.studentId).HasMaxLength(15).HasColumnName("studentid");
        });
        

        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.subjectId).HasName("subject_pkey");

            entity.ToTable("subject", "secretary");

            entity.Property(e => e.subjectId)
                .HasMaxLength(15)
                .HasDefaultValueSql("secretary.generate_subject_id()")
                .HasColumnName("subjectid");

            entity.Property(e => e.degreeId).HasColumnName("degreeid");
            entity.Property(e => e.name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
            entity.Property(e => e.initials).HasMaxLength(10).HasColumnName("initials");
        });

        modelBuilder.Entity<DegreeDataEntity>(entity =>
        {
            entity.HasKey(e => e.degreeDataId).HasName("degreecatalog_pkey");

            entity.ToTable("degreecatalog", "secretary");

            entity.Property(e => e.degreeDataId).HasColumnName("degreecatalogid");
            entity.Property(e => e.label).HasMaxLength(50).HasColumnName("label");
            entity.Property(e => e.modality).HasColumnName("modality");

            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.degreeDataId);
        });

        modelBuilder.Entity<SubjectDataEntity>(entity =>
        {
            entity.HasKey(e => e.subjectDataId).HasName("subjectcatalog_pkey");

            entity.ToTable("subjectcatalog", "secretary");

            entity.Property(e => e.subjectDataId).HasColumnName("subjectcatalogid");
            entity.Property(e => e.degreeDataId).HasColumnName("degreecatalogid");
            entity.Property(e => e.name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
            entity.Property(e => e.initials).HasMaxLength(10).HasColumnName("initials");
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
    }
}