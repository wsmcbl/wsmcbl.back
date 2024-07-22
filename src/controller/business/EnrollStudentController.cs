using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory), IEnrollStudentController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.studentDao!.getAll();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        var student = await daoFactory.studentDao!.getById(studentId);

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
        daoFactory.studentDao!.create(student);
        await daoFactory.execute();

        var schoolYear = await daoFactory.schoolyearDao!.getNewSchoolYear();
        var academyStudent = new model.academy.StudentEntity
            .Builder(student.studentId!, enrollmentId)
            .setSchoolyear(schoolYear.id!)
            .isNewEnroll()
            .build();
        
        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var printController = new PrintDocumentsController(daoFactory);
        return await printController.getEnrollDocument(studentId);
    }
}