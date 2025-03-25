using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CorrectEducationalLevelController : BaseController
{
    private readonly CollectTariffController collectTariffController; 
    public CorrectEducationalLevelController(DaoFactory daoFactory) : base(daoFactory)
    {
        collectTariffController = new CollectTariffController(daoFactory);
    }
    
    public async Task<StudentEntity> getStudentById(string studentId)
    {
        return await collectTariffController.getStudentById(studentId);
    }
    
    public async Task<StudentEntity> changeEducationalLevel(string studentId, int level)
    {
        if (await daoFactory.academyStudentDao!.isEnrolled(studentId))
        {
            throw new ConflictException("The student is already enroll. This operation can not be performed.");
        }

        var student = await collectTariffController.getStudentById(studentId);
        student.updateEducationalLevel(level);
        await daoFactory.ExecuteAsync();
        
        var debt = student.getCurrentRegistrationTariffDebt();
        await daoFactory.debtHistoryDao!.deleteAsync(debt);

        await daoFactory.debtHistoryDao.createRegistrationDebtByStudent(student);

        return student;
    }
}

