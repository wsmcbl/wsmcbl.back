using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.database.service;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.database;

public class StudentDaoPostgres : GenericDaoPostgres<StudentEntity, string>, IStudentDao
{
    public StudentDaoPostgres(PostgresContext context) : base(context)
    {
    }

    public async Task<StudentEntity> getFullById(string id)
    {
        var entity = await entities.Where(e => e.studentId == id)
            .Include(e => e.tutor)
            .Include(e => e.file)
            .Include(e => e.parents)
            .Include(e => e.measurements)
            .FirstOrDefaultAsync();
        
        if (entity == null)
        {
            throw new EntityNotFoundException("StudentEntity", id);
        }

        return entity;
    }

    public async Task<StudentEntity?> findDuplicateOrNull(StudentEntity student)
    {
        var list = await entities.Where(e => student.name == e.name)
            .AsNoTracking().ToListAsync();
        return list.Find(e => student.getStringData().Equals(e.getStringData()));
    }

    public async Task<PagedResult<StudentRegisterView>> getPaginatedStudentRegisterView(StudentPagedRequest request)
    {
        var query = context.GetQueryable<StudentRegisterView>();
        
        if (request.isActive != null)
        {
            query = query.Where(e => e.isActive == (bool)request.isActive);
        }
        
        var pagedService = new PagedService<StudentRegisterView>(query, searchInStudentRecordView);
        
        request.setDefaultSort("fullName");   
        return await pagedService.getPaged(request);
    }

    public async Task<List<StudentRegisterView>> getStudentRegisterListForCurrentSchoolyear()
    {
        var daoFactory = new DaoFactoryPostgres(context);

        var current = await daoFactory.schoolyearDao!.getCurrentOrNew();
        return await context.Set<StudentRegisterView>().Where(e => e.schoolyearId == current.id).ToListAsync();
    }

    public async Task<PagedResult<StudentView>> getPaginatedStudentView(StudentPagedRequest request)
    {
        var query = context.GetQueryable<StudentView>();
        
        if (request.isActive != null)
        {
            query = query.Where(e => e.isActive == (bool)request.isActive);
        }
        
        var pagedService = new PagedService<StudentView>(query, searchInStudentView);
        
        request.setDefaultSort("fullName");   
        return await pagedService.getPaged(request);
    }
    
    private static IQueryable<StudentView> searchInStudentView(IQueryable<StudentView> query, string search)
    { 
        var value = $"%{search}%";
        
        return query.Where(e =>
           EF.Functions.Like(e.studentId, value) ||
           EF.Functions.Like(e.fullName.ToLower(), value) ||
           EF.Functions.Like(e.tutor.ToLower(), value) ||
           (e.schoolyear != null && EF.Functions.Like(e.schoolyear.ToLower(), value)) ||
           (e.enrollment != null && EF.Functions.Like(e.enrollment.ToLower(), value)));
    }

    private static IQueryable<StudentRegisterView> searchInStudentRecordView(IQueryable<StudentRegisterView> query, string search)
    {
        var value = $"%{search}%";
        
        return query.Where(e =>
            EF.Functions.Like(e.studentId, value) ||
            EF.Functions.Like(e.fullName.ToLower(), value) ||
            EF.Functions.Like(e.tutor.ToLower(), value) ||
            EF.Functions.Like(e.address.ToLower(), value) ||
            (e.minedId != null && EF.Functions.Like(e.minedId.ToLower(), value)) ||
            (e.father != null && EF.Functions.Like(e.father.ToLower(), value)) ||
            (e.mother != null && EF.Functions.Like(e.mother.ToLower(), value)) ||
            (e.schoolyear != null && EF.Functions.Like(e.schoolyear.ToLower(), value)) ||
            (e.degree != null && EF.Functions.Like(e.degree.ToLower(), value)) ||
            (e.diseases != null && EF.Functions.Like(e.diseases.ToLower(), value)) ||
            (e.educationalLevel != null && EF.Functions.Like(e.educationalLevel.ToLower(), value)));
    }

    public async Task updateAsync(StudentEntity? entity)
    {
        if (entity == null)
        {
            return;
        }

        var existingStudent = await getById(entity.studentId!);
        if (existingStudent == null)
        {
            throw new EntityNotFoundException("StudentEntity", entity.studentId);
        }

        existingStudent.update(entity);
        update(existingStudent);
    }

    public new async Task deleteAsync(StudentEntity entity)
    {
        FormattableString query =$"delete from secretary.schoolyear_student where studentid = {entity.studentId};";
        await context.Database.ExecuteSqlAsync(query);
        
        await base.deleteAsync(entity);
    }
}