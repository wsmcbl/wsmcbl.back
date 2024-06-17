using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

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