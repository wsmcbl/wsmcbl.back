using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class GradeDaoPostgres(PostgresContext context) : GenericDaoPostgres<GradeEntity, int>(context), IGradeDao
{
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