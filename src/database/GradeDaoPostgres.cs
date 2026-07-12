using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
{
    public async Task createGradesBulk(List<GradeEntity> gradeList)
    {
        if (gradeList != null && gradeList.Count > 0)
        {
            await entities.AddRangeAsync(gradeList);
        }
        
        await saveAsync();
    }
    
    public async Task<List<GradeEntity>> getExistingGrades(string studentId, List<int> subjectPartialIds)
    {
        return await entities
            .Where(e => e.studentId == studentId && subjectPartialIds.Contains(e.subjectPartialId))
            .ToListAsync();
    }
    
    public async Task addRange(SubjectPartialEntity subjectPartial, List<GradeEntity> gradeList)
    {
        DaoFactory daoFactory = new DaoFactoryPostgres(context);
        var subjectPartialIdsList = await daoFactory.subjectPartialDao!.getIdListBySubject(subjectPartial);

        var currentGradeList = await entities.Where(e => subjectPartialIdsList.Contains(e.subjectPartialId))
            .ToListAsync();

        foreach (var currentGrade in currentGradeList)
        {
            var newGrade = gradeList.First(e => e.gradeId == currentGrade.gradeId);
            currentGrade.updateGrades(newGrade.grade, newGrade.conductGrade);
        }
    }
}