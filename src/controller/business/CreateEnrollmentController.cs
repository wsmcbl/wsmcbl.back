using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }

    public async Task<List<DegreeEntity>> getDegreeList()
    {
        return await daoFactory.degreeDao!.getAll();
    }

    public async Task<DegreeEntity?> getDegreeById(string degreeId)
    {
        var degree = await daoFactory.degreeDao!.getById(degreeId);

        if (degree == null)
        {
            throw new EntityNotFoundException("Degree", degreeId);
        }

        return degree;
    }

    public async Task<DegreeEntity> createEnrollments(string degreeId, int quantity)
    {
        if (quantity is > 7 or < 1)
        {
            throw new BadRequestException("Quantity in not valid");
        }

        var degree = await daoFactory.degreeDao!.getById(degreeId);
        if (degree == null)
        {
            throw new EntityNotFoundException("Degree", degreeId);
        }

        degree.createEnrollments(quantity);
        
        daoFactory.enrollmentDao!.createRange(degree.enrollmentList);
        await daoFactory.execute();
        
        return degree;
    }

    public async Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity enrollment)
    {
        var existingEntity = await daoFactory.enrollmentDao!.getById(enrollment.enrollmentId!);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollment.enrollmentId);
        }

        existingEntity.update(enrollment);
        daoFactory.enrollmentDao!.update(existingEntity);
        await daoFactory.execute();

        var subjectList = await daoFactory.subjectDao!.getByEnrollmentId(existingEntity.enrollmentId!);
        foreach (var item in subjectList)
        {
            item.teacherId = "tch-001";
            daoFactory.subjectDao.update(item);
        }

        await daoFactory.execute();

        return existingEntity;
    }    
}