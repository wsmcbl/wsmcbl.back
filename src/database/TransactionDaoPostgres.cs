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
            join student in context.Set<StudentEntity>() on transaction.studentId equals student.studentId
            join secretaryStudent in context.Set<model.secretary.StudentEntity>() on student.studentId equals secretaryStudent.studentId
            join enrollment in context.Set<EnrollmentEntity>() on student.enrollmentId equals enrollment.enrollmentId into studentEnrollments
            from enrollment in studentEnrollments.DefaultIfEmpty()
            where transaction.total > 0
            select new
            {
                transaction,
                student,
                secretaryStudent,
                enrollmentLabel = enrollment != null ? enrollment.label : "Sin matrícula"
            };

        var list = await query.ToListAsync();

        var result = new List<(TransactionEntity, StudentEntity)>();
        foreach (var item in list)
        {
            item.student.setStudent(item.secretaryStudent);
            item.student.enrollmentLabel = item.enrollmentLabel;
            result.Add((item.transaction, item.student));
        }

        return result;
    }
}