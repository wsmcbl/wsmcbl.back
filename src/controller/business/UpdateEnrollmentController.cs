using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class UpdateEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity enrollment)
    {
        var result = await daoFactory.enrollmentDao!.getById(enrollment.enrollmentId!);
        if (result is null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollment.enrollmentId);
        }
        
        return result;
    }
}