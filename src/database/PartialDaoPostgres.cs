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
    
    public async Task<List<PartialEntity>> getListInCurrentSchoolyear()
    {
        var semesterList = await daoFactory.semesterDao!.getListInCurrentSchoolyear();
        var idList = semesterList.Select(e => e.semesterId).ToList();

        return await entities.Where(e => idList.Contains(e.semesterId)).ToListAsync();
    }

    public async Task<List<PartialEntity>> getListByEnrollmentId(string enrollmentId)
    {
        var partialList = await getListInCurrentSchoolyear();

        foreach (var item in partialList)
        {
            item.subjectPartialList =
                await daoFactory.subjectPartialDao!.getByPartialAndEnrollmentId(item.partialId, enrollmentId);
        }

        return partialList;
    }
}