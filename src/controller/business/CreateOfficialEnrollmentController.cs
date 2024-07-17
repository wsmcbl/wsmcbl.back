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
    {}

    public async Task createSchoolYear(GradeEntity grade, List<TariffEntity> tariffList)
    {
        
    }

    public async Task createSubject(SubjectEntity subjectList)
    {
        throw new NotImplementedException();
    }

    public async Task createTariff(TariffEntity tariff)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAll();
    }

    public async Task<List<SchoolYearEntity>> getSchoolYearList()
    {
        throw new NotImplementedException();
    }

    public async Task<SchoolYearEntity> getNewSchoolYearInformation()
    {
        throw new NotImplementedException();
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
    
    
    
    

    public async Task<GradeEntity?> getGradeById(int gradeId)
    {
        return await daoFactory.gradeDao!.getById(gradeId);
    }

    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }

    public async Task createEnrollments(int gradeId, int quantity)
    {
        throw new NotImplementedException();
    }

    public async Task updateEnrollment(EnrollmentEntity enrollment)
    {
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();
    }
}