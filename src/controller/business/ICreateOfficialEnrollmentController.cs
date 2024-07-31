using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<TeacherEntity>> getTeacherList();
    public Task<List<GradeEntity>> getGradeList();
    public Task<GradeEntity?> getGradeById(string gradeId);
    public Task<List<SchoolYearEntity>> getSchoolYearList();
    public Task<SchoolYearEntity> getNewSchoolYearInformation();
    public Task createSchoolYear(List<GradeEntity> gradeList, List<TariffEntity> tariffList);
    public Task<TariffDataEntity> createTariff(TariffDataEntity tariff);
    public Task<SubjectDataEntity> createSubject(SubjectDataEntity subject);
    public Task createEnrollments(string gradeId, int quantity);
    public Task updateEnrollment(EnrollmentEntity enrollment);
}