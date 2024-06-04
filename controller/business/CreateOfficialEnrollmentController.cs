using wsmcbl.back.model.dao;
using wsmcbl.back.model.secretary;

namespace wsmcbl.back.controller.business;

public class CreateOfficialEnrollmentController : BaseController, ICreateOfficialEnrollmentController
{
    public CreateOfficialEnrollmentController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
        
    public Task<List<StudentEntity>> getStudentList()
    {
        return daoFactory.studentDao<StudentEntity>()!.getAll();
    }

    public Task saveStudent(StudentEntity student)
    {
        student.isActive = true;
        student.schoolYear = DateTime.Now.Year.ToString();
        return daoFactory.studentDao<StudentEntity>()!.create(student);
    }
}