using Microsoft.EntityFrameworkCore;
using wsmcbl.back.database.model.utils;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.database;

public partial class PostgresContext : DbContext
{
    public virtual DbSet<StudentEntity> Student { get; set; } = null!;
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
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "accounting");

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
            entity.Property(e => e.schoolYear)
                .HasColumnName("schoolyear");
            entity.Property(e => e.surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.enrollment)
                .HasMaxLength(20)
                .HasColumnName("enrollmentid");
            
            entity.HasMany(s => s.transactions)
                .WithOne()
                .HasForeignKey(s => s.studentId);
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
                .HasMaxLength(100)
                .HasColumnName("concept");
        });
        
        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.transactionId).HasName("transaction_pkey");

            entity.ToTable("transaction", "accounting");

            entity.Property(e => e.transactionId)
                .HasMaxLength(100)
                .HasColumnName("transactionid");
            entity.Property(e => e.cashierId)
                .HasMaxLength(100)
                .HasColumnName("cashierid");
            entity.Property(e => e.dateTime)
                .HasColumnName("date");
            entity.Property(e => e.discount)
                .HasColumnName("discount");
            entity.Property(e => e.studentId)
                .HasMaxLength(100)
                .HasColumnName("studentid");
            entity.Property(e => e.total)
                .HasColumnName("total");
            
            entity.HasMany(t => t.tariffs)
                .WithMany();
            
            entity.HasMany(t => t.tariffs)
                .WithMany()
                .UsingEntity(j => j.ToTable("tariff_transaction", "accounting"));
        });
        
    }
}
