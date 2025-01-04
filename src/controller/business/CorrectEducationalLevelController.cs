using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CorrectEducationalLevelController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<StudentEntity> getStudentById(string studentId)
    {
        var collectTariffController = new CollectTariffController(daoFactory);
        return await collectTariffController.getStudentById(studentId);
    }
    
    public async Task changeEducationalLevel(string studentId, int level)
    {
        if (await daoFactory.academyStudentDao!.hasAEnroll(studentId))
        {
            throw new ConflictException("The student is already enroll. This operation can not be performed.");
        }
        
        var student = await daoFactory.accountingStudentDao!.getById(studentId);
        if (student == null)
        {
            throw new EntityNotFoundException("student", studentId);
        }

        var debt = student.getCurrentRegistrationTariffDebt();
        await daoFactory.debtHistoryDao!.delete(debt);
        
        student.updateEducationalLevel(level);
        await daoFactory.execute();

        await daoFactory.debtHistoryDao.addRegistrationTariffDebtByStudent(student);
    }
}

