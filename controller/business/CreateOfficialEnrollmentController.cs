using wsmcbl.back.model.secretary;

namespace wsmcbl.back.controller.business;

public class CreateOfficialEnrollmentController : ICreateOfficialEnrollmentController
{
    private IStudentDao studentDao;

    public CreateOfficialEnrollmentController(IStudentDao studentDao)
    {
        this.studentDao = studentDao;
    }

    public Task<List<StudentEntity>> getStudentList()
    {
        return studentDao.getAll();
    }
}