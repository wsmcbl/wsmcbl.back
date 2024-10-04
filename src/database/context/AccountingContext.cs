using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database.context;

internal class AccountingContext
{
    private readonly ModelBuilder modelBuilder;

    public AccountingContext(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void create()
    {
        modelBuilder.Entity<CashierEntity>(entity =>
        {
            entity.HasKey(e => e.cashierId);
            entity.ToTable("cashier", "accounting");

            entity.Property(e => e.cashierId).HasMaxLength(15).HasColumnName("cashierid");
            entity.Property(e => e.userId).HasMaxLength(15).HasColumnName("userid");
            
            entity.HasOne(c => c.user).WithMany().HasForeignKey(c => c.userId);
        });
        
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.HasKey(e => e.studentId).HasName("student_pkey");

            entity.ToTable("student", "accounting");

            entity.Ignore(d => d.enrollmentLabel);

            entity.Property(e => e.studentId).HasMaxLength(20).HasColumnName("studentid");
            entity.Property(e => e.discountId).HasColumnName("discountid");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");

            entity.HasOne(d => d.discount).WithMany()
                .HasForeignKey(d => d.discountId);

            entity.HasOne(d => d.student).WithOne()
                .HasForeignKey<StudentEntity>(d => d.studentId);
            
            entity.HasMany(s => s.transactions)
                .WithOne()
                .HasForeignKey(s => s.studentId);
        });

        modelBuilder.Entity<DiscountEntity>(entity =>
        {
            entity.HasKey(e => e.discountId).HasName("discount_pkey");

            entity.ToTable("discount", "accounting");

            entity.Property(e => e.discountId).ValueGeneratedNever().HasColumnName("discountid");
            entity.Property(e => e.amount).HasColumnName("amount");
            entity.Property(e => e.description).HasMaxLength(150).HasColumnName("description");
            entity.Property(e => e.tag).HasMaxLength(50).HasColumnName("tag");
        });

        modelBuilder.Entity<TariffEntity>(entity =>
        {
            entity.HasKey(e => e.tariffId).HasName("tariff_pkey");

            entity.ToTable("tariff", "accounting");

            entity.Property(e => e.tariffId).HasColumnName("tariffid");
            entity.Property(e => e.amount).HasColumnName("amount");
            entity.Property(e => e.concept).HasMaxLength(200).HasColumnName("concept");
            entity.Property(e => e.dueDate).HasColumnName("duedate");
            entity.Property(e => e.isLate).HasColumnName("late");
            entity.Property(e => e.schoolYear).HasMaxLength(4).HasColumnName("schoolyear");
            entity.Property(e => e.type).HasColumnName("typeid");
            entity.Property(e => e.educationalLevel).HasColumnName("educationallevel");
        });
        
        modelBuilder.Entity<TariffTypeEntity>(entity =>
        {
            entity.HasKey(e => e.typeId).HasName("tarifftype_pkey");

            entity.ToTable("tarifftype", "accounting");

            entity.Property(e => e.typeId).HasColumnName("typeid");
            entity.Property(e => e.description).HasMaxLength(50).HasColumnName("description");
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

            entity.HasMany(t => t.details).WithOne()
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
        
        modelBuilder.Entity<DebtHistoryEntity>(entity =>
        {
            entity.HasKey(e => new { e.studentId, e.tariffId }).HasName("debthistory_pkey");

            entity.ToTable("debthistory", "accounting");

            entity.Property(e => e.studentId)
                .HasMaxLength(20)
                .HasColumnName("studentid");
            entity.Property(e => e.tariffId).HasColumnName("tariffid");
            entity.Property(e => e.arrears).HasColumnName("arrear");
            entity.Property(e => e.debtBalance).HasColumnName("debtbalance");
            entity.Property(e => e.subAmount).HasColumnName("subamount");
            entity.Property(e => e.isPaid).HasColumnName("ispaid");
            entity.Property(e => e.schoolyear).HasMaxLength(20).HasColumnName("schoolyear");
            entity.Property(e => e.amount).ValueGeneratedOnAddOrUpdate().HasColumnName("amount");
            
            entity.HasOne(d => d.tariff).WithMany()
                .HasForeignKey(d => d.tariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("debthistory_tariffid_fkey");
        });
    }
}