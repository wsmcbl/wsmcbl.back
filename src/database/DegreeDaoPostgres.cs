using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.database.service;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class DegreeDaoPostgres : GenericDaoPostgres<DegreeEntity, string>, IDegreeDao
{
    private DaoFactory daoFactory { get; set; }

    public DegreeDaoPostgres(PostgresContext context) : base(context)
    {
        daoFactory = new DaoFactoryPostgres(context);
    }
    
    public new async Task<DegreeEntity?> getById(string id)
    {
        var degree = await entities
            .Where(e => e.degreeId == id)
            .Include(e => e.subjectList)
            .Include(e => e.enrollmentList)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (degree == null)
        {
            throw new EntityNotFoundException("DegreeEntity", id);
        }

        return degree;
    }
    
    public async Task<DegreeEntity?> getWithAllPropertiesById(string id)
    {
        var query = entities.Include(e => e.subjectList)
            .Include(e => e.enrollmentList)!
            .ThenInclude(e => e.studentList)!
                .ThenInclude(e => e.student)
            .Include(e => e.enrollmentList)!
                .ThenInclude(e => e.subjectList)
            .AsNoTracking();

        var degree = await query.FirstOrDefaultAsync(e => e.degreeId == id);
        if (degree == null)
        {
            throw new EntityNotFoundException("DegreeEntity", id);
        }

        return degree;
    }
    public async Task createRange(List<DegreeEntity> degreeList)
    {
        entities.AddRange(degreeList);
        await saveAsync();
    }

    public async Task<List<DegreeEntity>> getValidListForTheSchoolyear()
    {
        var schoolyear = await daoFactory.schoolyearDao!.getNewOrCurrent();

        var list = await entities
            .Where(e => e.schoolyearId == schoolyear.id)
            .Include(e => e.enrollmentList)
            .ToListAsync();

        return list.Where(e => e.enrollmentList!.Count != 0).ToList();
    }

    public async Task<List<DegreeEntity>> getAll(string schoolyearId, bool withStudentsInEnrollment = false)
    {
        var query = entities.Where(e => e.schoolyearId == schoolyearId);

        if (withStudentsInEnrollment)
        {
            query = query.Include(e => e.enrollmentList)!
                .ThenInclude(e => e.studentList)!
                .ThenInclude(e => e.student);
        }
        else
        {
            query = query.Include(e => e.enrollmentList);
        }

        return await query.ToListAsync();
    }

    public async Task<DegreeEntity?> getByEnrollmentId(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        return await entities.FirstOrDefaultAsync(e => e.degreeId == enrollment.degreeId);
    }
    
    public async Task<PagedResult<DegreeEntity>> getAll(PagedRequest request)
    {
        var query = context.GetQueryable<DegreeEntity>();

        var pagedService = new PagedService<DegreeEntity>(query, search);
        
        request.setDefaultSort("tag");
        return await pagedService.getPaged(request);
    }
    
    private IQueryable<DegreeEntity> search(IQueryable<DegreeEntity> query, string search)
    { 
        var value = $"%{search}%";
        
        return query.Where(e =>
            EF.Functions.Like(e.degreeId!, value) ||
            EF.Functions.Like(e.label.ToLower(), value) ||
            EF.Functions.Like(e.educationalLevel.ToLower(), value) ||
            EF.Functions.Like(e.schoolyearId, value));
    }
}