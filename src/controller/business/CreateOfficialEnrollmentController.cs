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

    public async Task saveStudent(StudentEntity student)
    {
        student.init();
        daoFactory.studentDao<StudentEntity>()!.create(student);
        await daoFactory.execute();
    }

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAll();
    }
}