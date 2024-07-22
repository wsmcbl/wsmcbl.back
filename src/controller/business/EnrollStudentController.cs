using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController(DaoFactory daoFactory, IPrintDocumentsController printController) : BaseController(daoFactory), IEnrollStudentController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.secretaryStudentDao!.getAll();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        var student = await daoFactory.secretaryStudentDao!.getById(studentId);

        if (student == null)
        {
            throw new EntityNotFoundException("Secretary Student", studentId);
        }

        return student;
    }

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAll();
    }

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId)
    {
        daoFactory.secretaryStudentDao!.create(student);

        var academyStudent = new model.academy.StudentEntity();
        daoFactory.studentDao!.create(new model.accounting.StudentEntity());
        await daoFactory.execute();

        return student;
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        return await printController.getEnrollDocument(studentId);
    }
}