using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<TeacherEntity>> getTeacherList();
    public Task<TeacherEntity?> getTeacherById(string teacherId);
    public Task<List<DegreeEntity>> getDegreeList();
    public Task<DegreeEntity?> getDegreeById(string degreeId);
    public Task<List<SchoolYearEntity>> getSchoolYearList();
    public Task<SchoolYearEntity> getNewSchoolYearInformation();
    public Task<SchoolYearEntity> createSchoolYear(List<DegreeEntity> degreeList, List<TariffEntity> tariffList);
    public Task<TariffDataEntity> createTariff(TariffDataEntity tariff);
    public Task<SubjectDataEntity> createSubject(SubjectDataEntity subject);
    public Task<DegreeEntity> createEnrollments(string degreeId, int quantity);
    public Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity enrollment);
    public Task assignTeacherGuide(string teacherId, string enrollmentId);
}