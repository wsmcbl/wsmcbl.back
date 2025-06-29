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
        return await getListBySemesterList(semesterList);
    }

    public async Task<List<PartialEntity>> getListBySchoolyearId(string schoolyearId)
    {
        var semesterList = await daoFactory.semesterDao!.getListBySchoolyearId(schoolyearId);
        return await getListBySemesterList(semesterList);
    }

    private async Task<List<PartialEntity>> getListBySemesterList(List<SemesterEntity> list)
    {
        var idList = list.Select(e => e.semesterId).ToList();
        return await entities.Where(e => idList.Contains(e.semesterId)).ToListAsync();
    }
}