using Microsoft.EntityFrameworkCore;
using wsmcbl.back.model.accounting;
using wsmcbl.back.model.config;

namespace wsmcbl.back.database;

public class PostgresContext : DbContext
{
    public virtual DbSet<StudentEntity> Student { get; set; } = null!;
    public virtual DbSet<CashierEntity> Cashier { get; set; } = null!;
    public virtual DbSet<UserEntity> User { get; set; } = null!;
    public virtual DbSet<TariffEntity> Tariff { get; set; } = null!;
    public virtual DbSet<TransactionEntity> Transaction { get; set; } = null!;

    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

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

        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "secretary");

            entity.Property(e => e.tutor)
                .HasColumnName("tutor");
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
            entity.HasMany(s => s.transactions)
                .WithOne()
                .HasForeignKey(s => s.studentId);
            entity.HasOne(e => e.discount)
                .WithMany()
                .HasForeignKey("discountid");
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

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.transactionId).HasName("transaction_pkey");

            entity.ToTable("transaction", "accounting");

            entity.Property(e => e.transactionId)
                .HasMaxLength(100)
                .HasColumnName("transactionid");
            entity.Property(e => e.studentId)
                .HasMaxLength(100)
                .HasColumnName("studentid");
            entity.Property(e => e.cashierId)
                .HasMaxLength(100)
                .HasColumnName("cashierid");
            entity.Property(e => e.date)
                .HasColumnName("date");
            entity.Property(e => e.total)
                .HasColumnName("total");

            entity.HasMany(t => t.details)
                .WithOne()
                .HasForeignKey(tt => tt.transactionId);
        });

        modelBuilder.Entity<TransactionTariffEntity>(entity =>
        {
            entity.HasKey(e => new { e.transactionId, e.tariffId }).HasName("transaction_tariff_pkey");

            entity.ToTable("transaction_tariff", "accounting");

            entity.Property(e => e.transactionId)
                .HasMaxLength(15)
                .HasColumnName("transactionid");
            entity.Property(e => e.tariffId)
                .HasMaxLength(15)
                .HasColumnName("tariffid");
            entity.Property(e => e.arrears).HasColumnName("arrears");
            entity.Property(e => e.discount).HasColumnName("discount");
            entity.Property(e => e.subTotal).HasColumnName("subtotal");
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
    }
}