using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

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

    public async Task<List<GradeEntity>> getGradeList()
    {
        return await daoFactory.gradeDao!.getAll();
    }

    public async Task<GradeEntity?> getGradeById(string gradeId)
    {
        var grade = await daoFactory.gradeDao!.getById(gradeId);
        
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
        var gradeList = await daoFactory.gradeDataDao!.getAll();
        var tariffList = await daoFactory.tariffDataDao!.getAll();

        var newSchoolYear = await daoFactory.schoolyearDao!.getNewSchoolYear();
        newSchoolYear.setGradeDataList(gradeList);
        newSchoolYear.setTariffDataList(tariffList);

        return newSchoolYear;
    }

    public async Task createSchoolYear(List<GradeEntity> gradeList, List<TariffEntity> tariffList)
    {
        if (gradeList.Count == 0 || tariffList.Count == 0)
        {
            throw new ArgumentException("GradeLis or TariffList are not valid");
        }

        var tariffsNotValid = tariffList.Where(e => e.amount < 1).ToList().Count;

        if (tariffsNotValid > 0)
        {
            throw new ArgumentException($"{tariffsNotValid} tariffs do not have a valid Amount.");
        }
        
        daoFactory.gradeDao!.createList(gradeList);
        daoFactory.tariffDao!.createList(tariffList);
        await daoFactory.execute();
    }

    public async Task createTariff(TariffDataEntity tariff)
    {
        daoFactory.tariffDataDao!.create(tariff);
        await daoFactory.execute();
    }

    public async Task createSubject(SubjectDataEntity subject)
    {
        daoFactory.subjectDataDao!.create(subject);
        await daoFactory.execute();
    }

    public async Task createEnrollments(string gradeId, int quantity)
    {
        if (quantity > 7 || quantity < 1)
        {
            throw new ArgumentException("Quantity in not valid");
        }
        
        var grade = await daoFactory.gradeDao!.getById(gradeId);

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
    }

    public async Task updateEnrollment(EnrollmentEntity enrollment)
    {
        daoFactory.enrollmentDao!.update(enrollment);
        await daoFactory.execute();
    }

    
    
    
    
    
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.secretaryStudentDao!.getAll();
    }

    public async Task saveStudent(StudentEntity student)
    {
        student.init();
        daoFactory.secretaryStudentDao!.create(student);
        await daoFactory.execute();
    }
}