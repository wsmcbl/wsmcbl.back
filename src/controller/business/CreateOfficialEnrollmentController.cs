using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

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

    public async Task createGrade(GradeEntity entity)
    {
        daoFactory.gradeDao!.create(entity);
        await daoFactory.execute();
    }

    public async Task updateGrade(GradeEntity entity)
    {
        daoFactory.gradeDao!.update(entity);
        await daoFactory.execute();
    }

    public async Task updateSubjects(int gradeId, List<SubjectEntity> list)
    {
        var grade = await daoFactory.gradeDao!.getById(gradeId);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", gradeId.ToString());
        }

        grade.setSubjects(list);
        daoFactory.gradeDao.update(grade);
        await daoFactory.execute();
    }

    public async Task<List<SubjectEntity>> getSubjectList()
    {
        return await daoFactory.subjectDao!.getAll();
    }

    public async Task updateEnrollment(EnrollmentEntity entity)
    {
        daoFactory.enrollmentDao!.update(entity);
        await daoFactory.execute();
    }

    public async Task<List<EnrollmentEntity>> getEnrollmentList()
    {
        return await daoFactory.enrollmentDao!.getAll();
    }

    public async Task<EnrollmentEntity> getEnrollment(string enrollmentId)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(enrollmentId);

        if (enrollment == null)
        {
            throw new EntityNotFoundException("EnrollmentEntity", enrollmentId);
        }

        return enrollment;
    }

    public async Task assignTeacher(string enrollmentId, string subjectId, TeacherEntity teacher)
    {
        var enrollment = await getEnrollment(enrollmentId);

        enrollment.assignTeacher(subjectId, teacher);
        
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();
    }
}