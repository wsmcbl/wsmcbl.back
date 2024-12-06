using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateOfficialEnrollmentController : BaseController
{
    public CreateOfficialEnrollmentController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<TeacherEntity>> getTeacherList()
    {
        return await daoFactory.teacherDao!.getAll();
    }

    public async Task<TeacherEntity?> getTeacherById(string teacherId)
    {
        return await daoFactory.teacherDao!.getById(teacherId);
    }

    public async Task<List<DegreeEntity>> getDegreeList()
    {
        return await daoFactory.degreeDao!.getAll();
    }

    public async Task<DegreeEntity?> getDegreeById(string degreeId)
    {
        var degree = await daoFactory.degreeDao!.getById(degreeId);

        if (degree == null)
        {
            throw new EntityNotFoundException("Degree", degreeId);
        }

        return degree;
    }

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

    public async Task<DegreeEntity> createEnrollments(string degreeId, int quantity)
    {
        if (quantity is > 7 or < 1)
        {
            throw new BadRequestException("Quantity in not valid");
        }

        var degree = await daoFactory.degreeDao!.getById(degreeId);
        degree!.createEnrollments(quantity);

        foreach (var enrollment in degree.enrollmentList!)
        {
            daoFactory.enrollmentDao!.create(enrollment);
            await daoFactory.execute();

            foreach (var subject in enrollment.subjectList!)
            {
                daoFactory.Detached(subject);
            }
        }

        return degree;
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

        var subjectList = await daoFactory.subjectDao!.getByEnrollmentId(existingEntity.enrollmentId!);
        await checkTeacherListById(enrollment.getListTeacherIdBySubject());

        foreach (var item in enrollment.subjectList!)
        {
            var subject = subjectList.Find(e => e.subjectId == item.subjectId);
            if (subject == null)
                continue;
            
            subject.teacherId = item.teacherId;
            daoFactory.subjectDao.update(subject);
        }

        await daoFactory.execute();

        return existingEntity;
    }

    private async Task checkTeacherListById(List<string> value)
    {
        var teacherList = await daoFactory.teacherDao!.getByListByIdList(value);

        foreach (var item in value)
        {
            var result = teacherList.FirstOrDefault(e => e.teacherId == item);
            if (result == null)
            {
                throw new EntityNotFoundException("Teacher", item);
            }
        }
    }
    
    public async Task<TeacherEntity> assignTeacherGuide(string teacherId, string enrollmentId)
    {
        var existingEntity = await daoFactory.enrollmentDao!.getById(enrollmentId);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException("Enrollment", enrollmentId);
        }
        
        var teacher = await daoFactory.teacherDao!.getById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("Teacher", teacherId);
        }
        
        if(teacherId == existingEntity.teacherId)
            return teacher;
        
        teacher.isGuide = true;
        daoFactory.teacherDao.update(teacher);
        await daoFactory.execute();

        return teacher;
    }
}