using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
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
        return daoFactory.secretaryStudentDao!.getAll();
    }

    public async Task saveStudent(StudentEntity student)
    {
        student.init();
        daoFactory.secretaryStudentDao!.create(student);
        await daoFactory.execute();
    }

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAll();
    }

    public async Task<int> createGrade(GradeEntity entity)
    {
        await entity.setSubjects(daoFactory.subjectDao!, []);
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

    public async Task<List<SubjectEntity>> getSubjectListByGrade()
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
        var teacher = await daoFactory.teacherDao!.getById(teacherId);

        if (teacher == null)
        {
            throw new EntityNotFoundException("teacher", teacherId);
        }
        
        var enrollment = await getEnrollment(enrollmentId);
        var subject = await daoFactory.academySubjectDao!.getById(subjectId);

        if (subject == null)
        {
            throw new EntityNotFoundException("subject", subjectId);
        }
        
        teacher.assignSubject(subject);
        enrollment.assignSubject(subject);
        
        daoFactory.teacherDao!.update(teacher);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();
    }
    
    public async Task<List<SchoolYearEntity>> getSchoolYearList()
    {
        throw new NotImplementedException();
    }

    public async Task<SchoolYearEntity> getNewSchoolYearInformation()
    {
        throw new NotImplementedException();
    }

    public async Task createSubject(List<TariffEntity> getTariffList)
    {
        throw new NotImplementedException();
    }
}