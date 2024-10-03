using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateStudentProfileController(DaoFactory daoFactory)
    : BaseController(daoFactory), ICreateStudentProfileController
{
    public async Task<StudentEntity> createStudent(StudentEntity student, StudentTutorEntity tutor)
    {
        if (await isStudentExists(student, tutor))
        {
            throw new ConflictException("The student profile already exist.");
        }

        daoFactory.studentDao!.create(student);
        await daoFactory.execute();
        
        return student;
    }

    private async Task<bool> isStudentExists(StudentEntity student, StudentTutorEntity tutor)
    {
        var studentList = await daoFactory.studentDao!.getAll();

        foreach (var item in studentList)
        {
            if (student.fullName() == item.fullName())
                return true;
        }

        var tutorList = await daoFactory.studentTutorDao!.getAll();

        // Revisar
        foreach (var item in tutorList)
        {
            if (item.name == tutor.name)
                return false;
        }
        
        return false;
    }
}