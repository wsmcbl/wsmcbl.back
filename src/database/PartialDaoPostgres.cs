using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class PartialDaoPostgres : GenericDaoPostgres<PartialEntity, int>, IPartialDao
{
    private DaoFactory daoFactory { get; set; }
    
    public PartialDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
    public async Task<List<PartialEntity>> getListForCurrentSchoolyear()
    {
        var semesterList = await daoFactory.semesterDao!.getListForCurrentSchoolyear();
        var idList = semesterList.Select(e => e.semesterId).ToList();

        return await entities.Where(e => idList.Contains(e.semesterId)).ToListAsync();
    }

    public async Task<List<PartialEntity>> getListByEnrollmentId(string enrollmentId)
    {
        var partialList = await getListForCurrentSchoolyear();

        foreach (var item in partialList)
        {
            item.subjectPartialList =
                await daoFactory.subjectPartialDao!.getListByPartialIdAndEnrollmentId(item.partialId, enrollmentId);
        }

        return partialList;
    }
}