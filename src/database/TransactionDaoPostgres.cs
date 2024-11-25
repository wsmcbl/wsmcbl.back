using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.database;

public class TransactionDaoPostgres(PostgresContext context) : GenericDaoPostgres<TransactionEntity, string>(context), ITransactionDao
{
    public override void create(TransactionEntity entity)
    {
        if (!entity.haveValidContent())
        {
            throw new IncorrectDataBadRequestException("Transaction");
        }

        entity.computeTotal();
        base.create(entity);
    }

    public async Task<List<(TransactionEntity transaction, StudentEntity student)>> getByRange(DateTime start, DateTime end)
    {
        var query = from transaction in entities
            join secretaryStd in context.Set<model.secretary.StudentEntity>() on transaction.studentId equals secretaryStd.studentId
            join academyStd in context.Set<StudentEntity>() on secretaryStd.studentId equals academyStd.studentId into academyStdList
            from academyStd in academyStdList.DefaultIfEmpty()
            join enrollment in context.Set<EnrollmentEntity>() on academyStd.enrollmentId equals enrollment.enrollmentId into studentEnrollments
            from enrollment in studentEnrollments.DefaultIfEmpty()
            where transaction.total > 0
            select new
            {
                transaction,
                secretaryStudent = secretaryStd,
                enrollmentLabel = enrollment != null ? enrollment.label : "Sin matrícula"
            };

        var list = await query.ToListAsync();

        var result = new List<(TransactionEntity, StudentEntity)>();
        foreach (var item in list)
        {
            var student = new StudentEntity();
            student.setStudent(item.secretaryStudent);
            student.enrollmentLabel = item.enrollmentLabel;
            result.Add((item.transaction, student));
        }

        return result;
    }
}