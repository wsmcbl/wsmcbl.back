using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<TeacherEntity>> getTeacherList();
    public Task<List<DegreeEntity>> getGradeList();
    public Task<DegreeEntity?> getGradeById(string gradeId);
    public Task<List<SchoolYearEntity>> getSchoolYearList();
    public Task<SchoolYearEntity> getNewSchoolYearInformation();
    public Task<SchoolYearEntity> createSchoolYear(List<DegreeEntity> gradeList, List<TariffEntity> tariffList);
    public Task<TariffDataEntity> createTariff(TariffDataEntity tariff);
    public Task<SubjectDataEntity> createSubject(SubjectDataEntity subject);
    public Task<DegreeEntity> createEnrollments(string gradeId, int quantity);
    public Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity enrollment);
    public Task assignTeacherGuide(string teacherId, string enrollmentId);
}