using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateOfficialEnrollmentController : BaseController, ICreateOfficialEnrollmentController
{
    public CreateOfficialEnrollmentController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }

    public async Task<List<DegreeEntity>> getGradeList()
    {
        return await daoFactory.degreeDao!.getAll();
    }

    public async Task<DegreeEntity?> getGradeById(string gradeId)
    {
        var grade = await daoFactory.degreeDao!.getById(gradeId);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", gradeId);
        }

        return grade;
    }

    public async Task<List<SchoolYearEntity>> getSchoolYearList()
    {
        return await daoFactory.schoolyearDao!.getAll();
    }

    public async Task<SchoolYearEntity> getNewSchoolYearInformation()
    {
        var gradeList = await daoFactory.degreeDataDao!.getAll();
        var tariffList = await daoFactory.tariffDataDao!.getAll();

        var newSchoolYear = await daoFactory.schoolyearDao!.getNewSchoolYear();
        newSchoolYear.setGradeDataList(gradeList);
        newSchoolYear.setTariffDataList(tariffList);

        return newSchoolYear;
    }

    public async Task<SchoolYearEntity> createSchoolYear(List<DegreeEntity> gradeList, List<TariffEntity> tariffList)
    {
        if (gradeList.Count == 0 || tariffList.Count == 0)
        {
            throw new BadRequestException("GradeLis or TariffList are not valid");
        }

        var tariffsNotValid = tariffList.Where(e => e.amount < 1).ToList().Count;

        if (tariffsNotValid > 0)
        {
            throw new BadRequestException($"{tariffsNotValid} tariffs do not have a valid Amount.");
        }

        daoFactory.degreeDao!.createList(gradeList);
        daoFactory.tariffDao!.createList(tariffList);
        await daoFactory.execute();

        return await daoFactory.schoolyearDao!.getCurrentSchoolYear();
    }

    public async Task<TariffDataEntity> createTariff(TariffDataEntity tariff)
    {
        daoFactory.tariffDataDao!.create(tariff);
        await daoFactory.execute();
        return tariff;
    }

    public async Task<SubjectDataEntity> createSubject(SubjectDataEntity subject)
    {
        daoFactory.subjectDataDao!.create(subject);
        await daoFactory.execute();
        return subject;
    }

    public async Task<DegreeEntity> createEnrollments(string gradeId, int quantity)
    {
        if (quantity is > 7 or < 1)
        {
            throw new BadRequestException("Quantity in not valid");
        }

        var grade = await daoFactory.degreeDao!.getById(gradeId);

        if (grade == null)
        {
            throw new EntityNotFoundException("Grade", gradeId);
        }

        grade.createEnrollments(quantity);

        foreach (var enrollment in grade.enrollments!)
        {
            daoFactory.enrollmentDao!.create(enrollment);
            await daoFactory.execute();

            foreach (var subject in enrollment.subjectList!)
            {
                daoFactory.Detached(subject);
            }
        }

        return grade;
    }

    public async Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity enrollment)
    {
        var existingEntity = await daoFactory.enrollmentDao!.getById(enrollment.enrollmentId!);

        if (existingEntity == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollment.enrollmentId);
        }

        existingEntity.update(enrollment);
        daoFactory.enrollmentDao!.update(existingEntity);
        await daoFactory.execute();

        var list = await daoFactory.subjectDao!.getByEnrollmentId(existingEntity.enrollmentId!);

        foreach (var item in enrollment.subjectList!)
        {
            var subject = list.Find(e => e.subjectId == item.subjectId);

            if (subject == null) continue;
            subject.teacherId = item.teacherId;
            daoFactory.subjectDao.update(subject);
        }
        await daoFactory.execute();
        
        return existingEntity;
    }

    public async Task assignTeacherGuide(string teacherId, string enrollmentId)
    {
        var teacher = await daoFactory.teacherDao!.getById(teacherId);

        if (teacher != null)
        {
            teacher.enrollmentId = enrollmentId;
            teacher.isGuide = true;
            daoFactory.teacherDao.update(teacher);
            await daoFactory.execute();
        }
    }
}