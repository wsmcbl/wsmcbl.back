using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model;
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
            entity.Property(e => e.label).HasColumnName("label");
            entity.Property(e => e.schoolYear).HasColumnName("schoolyear");
            entity.Property(e => e.quantity).HasColumnName("quantity");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");
            entity.Property(e => e.tag).HasColumnName("tag");

            entity.HasMany(e => e.enrollmentList).WithOne()
                .HasForeignKey(d => d.degreeId);

            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.degreeId);
        });

        modelBuilder.Entity<SchoolyearEntity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("schoolyear_pkey");

            entity.ToTable("schoolyear", "secretary");

            entity.Property(e => e.id).ValueGeneratedOnAdd()
                .HasDefaultValueSql("secretary.generate_schoolyear_id()").HasColumnName("schoolyearid");

            entity.Property(e => e.deadLine).HasColumnName("deadline");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            entity.Property(e => e.label).HasColumnName("label");
            entity.Property(e => e.startDate).HasColumnName("startdate");
            
            entity.HasMany(e => e.degreeList).WithOne().HasForeignKey(e => e.schoolYear);
            entity.HasMany(e => e.tariffList).WithOne().HasForeignKey(e => e.schoolYear);
            entity.HasMany(e => e.semesterList).WithOne().HasForeignKey(e => e.schoolyear);
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
            entity.Property(e => e.birthday).HasColumnName("birthday");
            entity.Property(e => e.sex).HasColumnName("sex");
            entity.Property(e => e.isActive).HasColumnName("studentstate");
            entity.Property(e => e.diseases).HasColumnName("diseases");
            entity.Property(e => e.religion).HasColumnName("religion");
            entity.Property(e => e.address).HasColumnName("address");
            entity.Property(e => e.tutorId).HasMaxLength(15).HasColumnName("tutorid");
            entity.Property(e => e.minedId).HasMaxLength(30).HasColumnName("minedid");
            entity.Property(e => e.profilePicture).HasColumnName("profileimage");
            entity.Property(e => e.accessToken).HasMaxLength(20).HasColumnName("accesstoken");
            
            entity.HasOne(e => e.tutor).WithMany().HasForeignKey(e => e.tutorId);
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
            
            entity.HasOne<StudentEntity>().WithOne(e => e.file)
                .HasForeignKey<StudentFileEntity>(e => e.studentId);
        });

        modelBuilder.Entity<StudentMeasurementsEntity>(entity =>
        {
            entity.HasKey(e => e.measurementId).HasName("studentmeasurements_pkey");

            entity.ToTable("studentmeasurements", "secretary");

            entity.Property(e => e.measurementId).HasColumnName("measurementid");
            entity.Property(e => e.height).HasColumnName("height");
            entity.Property(e => e.studentId).HasColumnName("studentid");
            entity.Property(e => e.weight).HasColumnName("weight");
            
            entity.HasOne<StudentEntity>().WithOne(e => e.measurements)
                .HasForeignKey<StudentMeasurementsEntity>(e => e.studentId);
        });

        modelBuilder.Entity<StudentParentEntity>(entity =>
        {
            entity.HasKey(e => e.parentId).HasName("studentparent_pkey");

            entity.ToTable("studentparent", "secretary");

            entity.Property(e => e.parentId)
                .HasDefaultValueSql("secretary.generate_parent_id()")
                .HasColumnName("parentid");
            
            entity.Property(e => e.idCard).HasMaxLength(25).HasColumnName("idcard");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.occupation).HasColumnName("occupation");
            entity.Property(e => e.studentId).HasColumnName("studentid");
            entity.Property(e => e.sex).HasColumnName("sex");
            
            entity.HasOne<StudentEntity>().WithMany(e => e.parents)
                .HasForeignKey(e => e.studentId);
        });

        modelBuilder.Entity<StudentTutorEntity>(entity =>
        {
            entity.HasKey(e => e.tutorId).HasName("studenttutor_pkey");

            entity.ToTable("studenttutor", "secretary");

            entity.Property(e => e.tutorId)
                .HasMaxLength(15)
                .HasDefaultValueSql("secretary.generate_tutor_id()")
                .HasColumnName("tutorid");
            
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.phone).HasColumnName("phone");
            entity.Property(e => e.email).HasColumnName("email");
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
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
            entity.Property(e => e.initials).HasColumnName("initials");
            entity.Property(e => e.areaId).HasColumnName("areaid");
            entity.Property(e => e.number).HasColumnName("number");
        });

        modelBuilder.Entity<SubjectAreaEntity>(entity =>
        {
            entity.HasKey(e => e.areaId).HasName("subjectarea_pkey");

            entity.ToTable("subjectarea", "secretary");

            entity.Property(e => e.areaId).HasColumnName("areaid");
            entity.Property(e => e.name).HasColumnName("name");
        });

        modelBuilder.Entity<DegreeDataEntity>(entity =>
        {
            entity.HasKey(e => e.degreeDataId).HasName("degreecatalog_pkey");

            entity.ToTable("degreecatalog", "secretary");

            entity.Property(e => e.degreeDataId).HasColumnName("degreecatalogid");
            entity.Property(e => e.label).HasColumnName("label");
            entity.Property(e => e.tag).HasColumnName("tag");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");

            entity.HasMany(e => e.subjectList).WithOne()
                .HasForeignKey(d => d.degreeDataId);
        });

        modelBuilder.Entity<SubjectDataEntity>(entity =>
        {
            entity.HasKey(e => e.subjectDataId).HasName("subjectcatalog_pkey");

            entity.ToTable("subjectcatalog", "secretary");

            entity.Property(e => e.subjectDataId).HasColumnName("subjectcatalogid");
            entity.Property(e => e.degreeDataId).HasColumnName("degreecatalogid");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.isMandatory).HasColumnName("ismandatory");
            entity.Property(e => e.semester).HasColumnName("semester");
            entity.Property(e => e.initials).HasColumnName("initials");
            entity.Property(e => e.areaId).HasColumnName("areaid");
            entity.Property(e => e.number).HasColumnName("number");
        });

        modelBuilder.Entity<TariffDataEntity>(entity =>
        {
            entity.HasKey(e => e.tariffDataId).HasName("tariffcatalog_pkey");

            entity.ToTable("tariffcatalog", "secretary");

            entity.Property(e => e.tariffDataId).HasColumnName("tariffcatalogid");
            entity.Property(e => e.amount).HasColumnName("amount");
            entity.Property(e => e.concept).HasColumnName("concept");
            entity.Property(e => e.dueDate).HasColumnName("duedate");
            entity.Property(e => e.typeId).HasColumnName("typeid");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");
        });

        createView();
    }

    private void createView()
    {
        modelBuilder.Entity<StudentView>(entity =>
        {
            entity.ToView("student_to_list_view", "secretary").HasNoKey();
            entity.Property(e => e.studentId).HasColumnName("studentid");
            entity.Property(e => e.fullName).HasColumnName("fullname");
            entity.Property(e => e.isActive).HasColumnName("studentstate");
            entity.Property(e => e.tutor).HasColumnName("tutor");
            entity.Property(e => e.schoolyear).HasColumnName("schoolyear");
            entity.Property(e => e.enrollment).HasColumnName("enrollment");
        });
        
        modelBuilder.Entity<StudentRegisterView>(entity =>
        {
            entity.ToView("student_register_view", "secretary").HasNoKey();
            entity.Property(e => e.studentId).HasColumnName("studentid");
            entity.Property(e => e.minedId).HasColumnName("minedid");
            entity.Property(e => e.fullName).HasColumnName("fullname");
            entity.Property(e => e.isActive).HasColumnName("isactive");
            entity.Property(e => e.sex).HasColumnName("sex");
            entity.Property(e => e.birthday).HasColumnName("birthday");
            entity.Property(e => e.diseases).HasColumnName("diseases");
            entity.Property(e => e.address).HasColumnName("address");
            entity.Property(e => e.height).HasColumnName("height");
            entity.Property(e => e.weight).HasColumnName("weight");
            entity.Property(e => e.tutor).HasColumnName("tutor");
            entity.Property(e => e.phone).HasColumnName("phone");
            entity.Property(e => e.father).HasColumnName("fathername");
            entity.Property(e => e.fatherIdCard).HasColumnName("fatheridcard");
            entity.Property(e => e.mother).HasColumnName("mothername");
            entity.Property(e => e.motherIdCard).HasColumnName("motheridcard");
            entity.Property(e => e.schoolyear).HasColumnName("schoolyear");
            entity.Property(e => e.schoolyearId).HasColumnName("schoolyearid");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");
            entity.Property(e => e.degree).HasColumnName("degree");
            entity.Property(e => e.degreePosition).HasColumnName("degreeposition");
            entity.Property(e => e.section).HasColumnName("section");
            entity.Property(e => e.sectionPosition).HasColumnName("sectionposition");
            entity.Property(e => e.enrollDate).HasColumnName("enrolldate");
            entity.Property(e => e.isRepeating).HasColumnName("isrepeating");
        });
    }
}