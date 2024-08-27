using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class EnrollStudentController(DaoFactory daoFactory) : BaseController(daoFactory), IEnrollStudentController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.studentDao!.getAllWithSolvency();
    }

    public async Task<StudentEntity> getStudentById(string studentId)
    {
        var result = await daoFactory.studentDao!.getByIdWithProperties(studentId);

        if (result == null)
        {
            throw new EntityNotFoundException("Student", studentId);
        }

        return result;
    }

    public async Task<List<DegreeEntity>> getDegreeList()
    {
        return await daoFactory.degreeDao!.getAllForTheCurrentSchoolyear();
    }

    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId)
    {
        await student.saveChanges(daoFactory);

        var academyStudent = await getNewAcademyStudent(student.studentId!, enrollmentId);
        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    private async Task<model.academy.StudentEntity> getNewAcademyStudent(string studentId, string enrollmentId)
    {
        var schoolYear = await daoFactory.schoolyearDao!.getNewSchoolYear();
        
        var academyStudent = new model.academy.StudentEntity(studentId, enrollmentId);
        
        academyStudent.setSchoolyear(schoolYear.id!);
        academyStudent.isNewEnroll();

        return academyStudent;
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getEnrollDocument(studentId);
    }
}