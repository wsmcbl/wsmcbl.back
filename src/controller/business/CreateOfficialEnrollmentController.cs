using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using ISubjectDao = wsmcbl.src.model.academy.ISubjectDao;
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

    public async Task<int> createGrade(GradeEntity entity, List<string> subjectIdsList)
    {
        await entity.setSubjects(daoFactory.subjectDao!, subjectIdsList);
        daoFactory.gradeDao!.create(entity);
        await daoFactory.execute();

        return entity.gradeId;
    }

    public async Task updateGrade(GradeEntity entity)
    {
        var grade = await daoFactory.gradeDao!.getById(entity.gradeId);

        if (grade == null)
        {
            throw new EntityNotFoundException("grade", entity.gradeId.ToString());
        }
        
        grade.init(entity.label, grade.schoolYear, grade.modality);
        grade.computeQuantity();
        daoFactory.gradeDao!.update(grade);
        await daoFactory.execute();
    }

    public async Task updateSubjects(int gradeId, List<string> subjectIdsList)
    {
        var grade = await daoFactory.gradeDao!.getById(gradeId);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", gradeId.ToString());
        }

        await grade.setSubjects(daoFactory.subjectDao!, subjectIdsList);
        daoFactory.gradeDao.update(grade);
        await daoFactory.execute();
    }

    public async Task<List<SubjectEntity>> getSubjectList()
    {
        return await daoFactory.subjectDao!.getAll();
    }

    public async Task updateEnrollment(EnrollmentEntity entity)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(entity.enrollmentId);

        if (enrollment == null)
        {
            throw new EntityNotFoundException("Enrollment", entity.enrollmentId);
        }

        enrollment.updateData(entity.label, entity.schoolYear, entity.section, entity.capacity);
        daoFactory.enrollmentDao!.update(enrollment);
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

    public async Task assignTeacher(string teacherId, string subjectId, string enrollmentId)
    {
        var enrollment = await getEnrollment(enrollmentId);

        await enrollment.subjectExistOrSet(subjectId, daoFactory.academySubjectDao!);
        await enrollment.teacherExistOrSet(teacherId, daoFactory.teacherDao!);
        enrollment.assignTeacher(subjectId, teacherId);
        
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();
    }
}