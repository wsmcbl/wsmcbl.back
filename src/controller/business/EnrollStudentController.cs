
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory), IEnrollStudentController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        throw new NotImplementedException();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GradeEntity>> getGradeList()
    {
        throw new NotImplementedException();
    }

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<object> printEnrollDocument(string studentId)
    {
        throw new NotImplementedException();
    }
}