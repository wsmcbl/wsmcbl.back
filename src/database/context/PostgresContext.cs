using Microsoft.EntityFrameworkCore;
using wsmcbl.src.model.config;

namespace wsmcbl.src.database.context;

public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var academy = new AcademyContext(modelBuilder);
        var accounting = new AccountingContext(modelBuilder);
        var secretary = new SecretaryContext(modelBuilder);
        var config = new ConfigContext(modelBuilder);
        
        academy.create();
        accounting.create();
        secretary.create();
        config.create();
        
        modelBuilder.HasSequence("enrollment_id_seq", "academy").StartsAt(10L);
        modelBuilder.HasSequence("grade_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("schoolyear_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("student_id_seq", "secretary");
        modelBuilder.HasSequence("subject_id_seq", "secretary").StartsAt(10L);
        modelBuilder.HasSequence("transaction_id_seq", "accounting").StartsAt(10L);
    }

    public IQueryable<T> GetQueryable<T>() where T : class
    {
        return Set<T>().AsNoTracking().AsQueryable();
    }
}