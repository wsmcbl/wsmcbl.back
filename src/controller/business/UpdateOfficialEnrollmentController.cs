using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateOfficialEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<DegreeEntity?> getDegreeById(string degreeId)
    {
        var degree = await daoFactory.degreeDao!.getFullById(degreeId);
        if (degree == null)
        {
            throw new EntityNotFoundException("DegreeEntity", degreeId);
        }

        if (degree.quantity == 0)
        {
            throw new BadRequestException($"DegreeEntity with id ({degreeId}) has not enrollments.");
        }
        
        return degree;
    }
    
    public async Task updateEnrollment(EnrollmentEntity value)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(value.enrollmentId!);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", value.enrollmentId);
        }

        enrollment.update(value);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.ExecuteAsync();
    }
}