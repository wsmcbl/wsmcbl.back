using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class UpdateOfficialEnrollmentController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<SchoolYearEntity>> getSchoolYearList()
    {
        return await daoFactory.schoolyearDao!.getAll();
    }

    public async Task<SchoolYearEntity> getNewSchoolYearInformation()
    {
        var degreeList = await daoFactory.degreeDataDao!.getAll();
        var tariffList = await daoFactory.tariffDataDao!.getAll();

        var newSchoolYear = await daoFactory.schoolyearDao!.getOrCreateNewSchoolyear();
        newSchoolYear.setDegreeDataList(degreeList);
        newSchoolYear.setTariffDataList(tariffList);

        return newSchoolYear;
    }

    public async Task<SchoolYearEntity> createSchoolYear(List<DegreeEntity> degreeList, List<TariffEntity> tariffList)
    {
        if (degreeList.Count == 0 || tariffList.Count == 0)
        {
            throw new BadRequestException("DegreeList or TariffList are not valid");
        }

        var tariffsNotValid = tariffList.Where(e => e.amount < 1).ToList().Count;

        if (tariffsNotValid > 0)
        {
            throw new BadRequestException($"{tariffsNotValid} tariffs do not have a valid Amount.");
        }

        daoFactory.degreeDao!.createList(degreeList);
        daoFactory.tariffDao!.createList(tariffList);
        await daoFactory.execute();

        return await daoFactory.schoolyearDao!.getOrCreateNewSchoolyear();
    }

    public async Task createSemester(SchoolYearEntity schoolyear, List<PartialEntity> partialList)
    {
        var firstSemester = createSemester(schoolyear.id!, 1, partialList);
        var secondSemester = createSemester(schoolyear.id!, 2, partialList);
        
        daoFactory.semesterDao!.create(firstSemester);
        daoFactory.semesterDao!.create(secondSemester);
        await daoFactory.execute();
    }

    private static SemesterEntity createSemester(string schoolyearId, int semester, IEnumerable<PartialEntity> partialList)
    {
        var result = new SemesterEntity
        {
            isActive = false,
            label = semester == 1 ? "I Semester" : "II Semester",
            semester = semester,
            schoolyear = schoolyearId,
            partialList = partialList.Where(e => e.semester == semester).ToList()
        };
        result.updateDeadLine();

        return result;
    }

    public async Task createExchangeRate(SchoolYearEntity schoolyear, double exchangeRate)
    {
        var entity = new ExchangeRateEntity
        {
            schoolyear = schoolyear.id!,
            value = exchangeRate
        };
        
        daoFactory.exchangeRateDao!.create(entity);
        await daoFactory.execute();
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

    public async Task<EnrollmentEntity> updateEnrollment(EnrollmentEntity value)
    {
        var enrollment = await daoFactory.enrollmentDao!.getById(value.enrollmentId!);
        if (enrollment == null)
        {
            throw new EntityNotFoundException("Enrollment", value.enrollmentId);
        }

        enrollment.update(value);
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();

        return enrollment;
    }
}