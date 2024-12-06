using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateStudentProfileController(DaoFactory daoFactory)
    : BaseController(daoFactory), IUpdateStudentProfileController
{
    public async Task updateStudent(StudentEntity student)
    {
        daoFactory.studentDao!.update(student);
        await daoFactory.execute();
    }
}