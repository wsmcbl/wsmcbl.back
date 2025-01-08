using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.database;

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
{
    public async Task addRange(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList)
    {
        var subjectPartialIdsList = await context.Set<SubjectPartialEntity>()
            .Where(e => e.enrollmentId == subjectPartial.enrollmentId
                        && e.teacherId == subjectPartial.teacherId
                        && e.partialId == subjectPartial.partialId)
            .Select(e => e.subjectPartialId)
            .ToListAsync();

        var currentGradeList = await entities
            .Where(e => subjectPartialIdsList.Contains(e.subjectPartialId))
            .ToListAsync();

        foreach (var currentGrade in currentGradeList)
        {
            var newGrade = gradeList.First(e => e.gradeId == currentGrade.gradeId);
            currentGrade.updateGrades(newGrade.grade, newGrade.conductGrade);
        }
    }
}