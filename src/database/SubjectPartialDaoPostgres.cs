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

    public async Task<List<int>> getIdListBySubject(SubjectPartialEntity subjectPartial)
    {
        return await entities
            .Where(e => e.enrollmentId == subjectPartial.enrollmentId
                        && e.teacherId == subjectPartial.teacherId
                        && e.partialId == subjectPartial.partialId)
            .Select(e => e.subjectPartialId)
            .ToListAsync();
    }

    public async Task<List<SubjectPartialEntity>> getListByPartialIdAndEnrollmentId(int partialId, string enrollmentId)
    {
        return await entities.AsNoTracking()
            .Where(e => e.partialId == partialId && e.enrollmentId == enrollmentId)
            .Include(e => e.gradeList)
            .ThenInclude(e => e.student)
            .ToListAsync();
    }
}