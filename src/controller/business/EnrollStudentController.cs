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

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAllForTheCurrentSchoolyear();
    }
    
    public async Task<StudentEntity> saveEnroll(StudentEntity student, string enrollmentId)
    {
        await daoFactory.studentDao!.updateAsync(student);
        await daoFactory.studentFileDao!.updateAsync(student.file!);
        await daoFactory.studentTutorDao!.updateAsync(student.tutor!);
        await daoFactory.studentMeasurementsDao!.updateAsync(student.measurements!);
        
        foreach (var parent in student.parents!)
        {
            await daoFactory.studentParentDao!.updateAsync(parent);
        }
        await daoFactory.execute();

        var schoolYear = await daoFactory.schoolyearDao!.getNewSchoolYear();
        var academyStudent = new model.academy.StudentEntity.Builder(student.studentId!, enrollmentId)
            .setSchoolyear(schoolYear.id!)
            .isNewEnroll()
            .build();

        daoFactory.academyStudentDao!.create(academyStudent);
        await daoFactory.execute();

        return student;
    }

    public async Task<byte[]> getEnrollDocument(string studentId)
    {
        var printController = new PrintDocumentController(daoFactory);
        return await printController.getEnrollDocument(studentId);
    }
}