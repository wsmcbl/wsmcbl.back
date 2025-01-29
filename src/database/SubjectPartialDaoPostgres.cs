using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class SubjectPartialDaoPostgres(PostgresContext context) :
    GenericDaoPostgres<SubjectPartialEntity, int>(context), ISubjectPartialDao
{
    public async Task<List<SubjectPartialEntity>> getListBySubject(SubjectPartialEntity subjectPartial)
    {
        var result = await entities
            .Where(e => e.teacherId == subjectPartial.teacherId
                        && e.enrollmentId == subjectPartial.enrollmentId
                        && e.partialId == subjectPartial.partialId)
            .Include(e => e.gradeList)
            .ToListAsync();

        if (result.Count == 0)
        {
            throw new ConflictException(
                $"There are no records of (SubjectPartialEntity) for the teacherId ({subjectPartial.teacherId})," +
                $" enrollmentId ({subjectPartial.enrollmentId})" +
                $" and partialId ({subjectPartial.partialId}).");
        }

        return result;
    }
}