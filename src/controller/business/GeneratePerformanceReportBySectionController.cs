using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GeneratePerformanceReportBySectionController : BaseController
{
    public GeneratePerformanceReportBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<StudentEntity>> getEnrollmentPerformanceByTeacherId(string teacherId)
    { 
        var teacher = await daoFactory.teacherDao!.getById(teacherId);
        if (teacher == null)
        {
            throw new EntityNotFoundException("TeacherEntity", teacherId);
        }

        await teacher.setCurrentEnrollment(daoFactory.schoolyearDao!);
        if (!teacher.hasCurrentEnrollment())
        {
            throw new EntityNotFoundException($"Entity of type (EnrollmentEntity) with teacher id ({teacherId}) not found.");
        }

        var enrollment = await daoFactory.enrollmentDao!.getFullById(teacher.getCurrentEnrollmentId());
        
        return enrollment.studentList!.ToList();
    }
}