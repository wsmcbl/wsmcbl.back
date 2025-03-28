using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<DegreeEntity>> getPaginatedDegree(PagedRequest request)
    {
        return await daoFactory.degreeDao!.getPaginated(request);
    }

    public async Task<DegreeEntity> createEnrollments(string degreeId, int quantity)
    {
        if (!DegreeEntity.isValidQuantity(quantity))
        {
            throw new IncorrectDataException("Quantity", "The value must be between 1 and 7.");
        }

        var degree = await daoFactory.degreeDao!.getById(degreeId);
        if (degree == null)
        {
            throw new EntityNotFoundException("DegreeEntity", degreeId);
        }

        degree.createEnrollments(quantity);
        await degree.saveEnrollments(daoFactory.enrollmentDao!);
        
        return degree;
    }

    public async Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity value)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(value.enrollmentId!);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", value.enrollmentId);
        }

        enrollment.section = value.section;
        enrollment.capacity = value.capacity;
        
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.ExecuteAsync();

        return enrollment;
    }    
}