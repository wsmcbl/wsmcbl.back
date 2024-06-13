using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;
using wsmcbl.back.model.config;
using Student_Accounting = wsmcbl.back.model.accounting.StudentEntity;
using Student_Secretary = wsmcbl.back.model.secretary.StudentEntity;

namespace wsmcbl.back.database;

public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
{
    public virtual DbSet<TariffEntity> Tariff { get; init; } = null!;
    public virtual DbSet<DebtHistoryEntity> DebtHistory { get; init; } = null!;
    public virtual DbSet<Student_Accounting> Student_accounting { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashierEntity>(entity =>
        {
            entity.HasKey(e => e.cashierId);
            entity.ToTable("cashier", "accounting");

            entity.Property(e => e.cashierId).HasMaxLength(15).HasColumnName("cashierid");
            entity.Property(e => e.userId).HasMaxLength(15).HasColumnName("userid");
            
            entity.HasOne(c => c.user)
                .WithMany()
                .HasForeignKey(c => c.userId);
        });
        
        modelBuilder.Entity<Student_Accounting>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "accounting");

            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasColumnName("studentid");
            entity.Property(e => e.discountId).HasColumnName("discountid");

            entity.HasOne(d => d.discount)
                .WithMany()
                .HasForeignKey(d => d.discountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_discountid_fkey");

            entity.HasOne(d => d.student)
                .WithOne()
                .HasForeignKey<Student_Accounting>(d => d.studentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_studentid_fkey");
            
            entity.HasMany(s => s.transactions)
                .WithOne()
                .HasForeignKey(s => s.studentId);
        });

        modelBuilder.Entity<Student_Secretary>(entity =>
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

        modelBuilder.Entity<DiscountEntity>(entity =>
        {
            entity.HasKey(e => e.discountId).HasName("discount_pkey");

            entity.ToTable("discount", "accounting");

            entity.Property(e => e.discountId)
                .ValueGeneratedNever()
                .HasColumnName("discountid");
            entity.Property(e => e.amount).HasColumnName("amount");
            entity.Property(e => e.description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.tag)
                .HasMaxLength(50)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<TariffEntity>(entity =>
        {
            entity.HasKey(e => e.tariffId).HasName("tariff_pkey");

            entity.ToTable("tariff", "accounting");

            entity.Property(e => e.tariffId)
                .HasColumnName("tariffid");
            entity.Property(e => e.amount)
                .HasColumnName("amount");
            entity.Property(e => e.concept)
                .HasMaxLength(200)
                .HasColumnName("concept");

            entity.Property(e => e.dueDate).HasColumnName("duedate");
            entity.Property(e => e.isLate).HasColumnName("late");
            entity.Property(e => e.schoolYear)
                .HasMaxLength(4)
                .HasColumnName("schoolyear");
            entity.Property(e => e.type).HasColumnName("typeid");
        });
        
        
        modelBuilder.Entity<TariffTypeEntity>(entity =>
        {
            entity.HasKey(e => e.typeId).HasName("tarifftype_pkey");

            entity.ToTable("tarifftype", "accounting");

            entity.Property(e => e.typeId).HasColumnName("typeid");
            entity.Property(e => e.description)
                .HasMaxLength(50)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.ToTable("transaction", "accounting");
            entity.HasKey(e => e.transactionId).HasName("transaction_pkey");

            entity.Property(e => e.transactionId)
                .HasDefaultValueSql("accounting.generate_transaction_id()")
                .HasColumnName("transactionid");
            
            entity.Property(e => e.studentId).HasMaxLength(100).HasColumnName("studentid");
            entity.Property(e => e.cashierId).HasMaxLength(100).HasColumnName("cashierid");
            entity.Property(e => e.date).HasColumnName("date");
            entity.Property(e => e.total).HasColumnName("total");

            entity.HasMany(t => t.details)
                .WithOne()
                .HasForeignKey(tt => tt.transactionId);
        });

        modelBuilder.Entity<TransactionTariffEntity>(entity =>
        {
            entity.HasKey(e => new { e.transactionId, e.tariffId }).HasName("transaction_tariff_pkey");

            entity.ToTable("transaction_tariff", "accounting");

            entity.Property(e => e.transactionId).HasMaxLength(20).HasColumnName("transactionid");
            entity.Property(e => e.tariffId).HasMaxLength(15).HasColumnName("tariffid");
            entity.Property(e => e.amount).HasColumnName("amount");
        });


        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.userId).HasName("user_pkey");

            entity.ToTable("user", "config");

            entity.Property(e => e.userId).HasMaxLength(15).HasColumnName("userid");
            entity.Property(e => e.email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.password).HasMaxLength(100).HasColumnName("password");
            entity.Property(e => e.secondName).HasMaxLength(50).HasColumnName("secondname");
            entity.Property(e => e.secondsurName).HasMaxLength(50).HasColumnName("secondsurname");
            entity.Property(e => e.surname).HasMaxLength(50).HasColumnName("surname");
            entity.Property(e => e.username).HasMaxLength(45).HasColumnName("username");
            entity.Property(e => e.isActive).HasColumnName("userstate");
        });
        
        modelBuilder.Entity<DebtHistoryEntity>(entity =>
        {
            entity.HasKey(e => new { e.studentId, e.tariffId }).HasName("debthistory_pkey");

            entity.ToTable("debthistory", "accounting");

            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasColumnName("studentid");
            entity.Property(e => e.tariffId).HasColumnName("tariffid");
            entity.Property(e => e.arrear).HasColumnName("arrear");
            entity.Property(e => e.debtBalance).HasColumnName("debtbalance");
            entity.Property(e => e.discount).HasColumnName("discount");
            entity.Property(e => e.isPaid).HasColumnName("ispaid");
            entity.Property(e => e.schoolyear)
                .HasMaxLength(20)
                .HasColumnName("schoolyear");
            
            entity.HasOne(d => d.tariff).WithMany()
                .HasForeignKey(d => d.tariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("debthistory_tariffid_fkey");
        });
    }
}