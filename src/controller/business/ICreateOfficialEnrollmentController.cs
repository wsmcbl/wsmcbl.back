using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<GradeEntity>> getGradeList();
    public Task<List<SchoolYearEntity>> getSchoolYearList();
    public Task<SchoolYearEntity> getNewSchoolYearInformation();
    
    public Task createTariff(TariffEntity tariff);
    public Task createSubject(SubjectEntity subjectList);
    public Task createSchoolYear(GradeEntity grade, List<TariffEntity> tariffList);
    
    
    
    public Task<List<StudentEntity>> getStudentList();
    public Task saveStudent(StudentEntity student);
    
    
    
    public Task createEnrollments(int gradeId, int quantity);
    public Task updateEnrollment(EnrollmentEntity enrollment);
    public Task<GradeEntity?> getGradeById(int gradeId);
    public Task<List<TeacherEntity>> getTeacherList();
}