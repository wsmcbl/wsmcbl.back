using wsmcbl.src.controller.service.sheet;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GenerateEvaluationStatsBySectionController : BaseController
{
    public GenerateEvaluationStatsBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<List<StudentEntity>> getStudentListByTeacherId(string teacherId, int partialId)
    {
        var controller = new GeneratePerformanceReportBySectionController(daoFactory);
        return await controller.getStudentListByTeacherId(teacherId, partialId);
    }
    
    public async Task<List<model.secretary.StudentEntity>> getInitialListByTeacherId(string teacherId)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var result = await daoFactory.academyStudentDao!.getListBeforeFirstPartial(enrollmentId);
        var initialList = result.Select(e => e.student).ToList();
        
        var list = await daoFactory.withdrawnStudentDao!.getListByEnrollmentId(enrollmentId, true);
        var withdrawnStudentList = list.Select(e => e.student!).ToList();
        
        return initialList.Union(withdrawnStudentList).ToList();
    }

    public async Task<List<SubjectPartialEntity>> getSubjectListByTeacherId(string teacherId, int partialId)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var partial = await daoFactory.partialDao!.getById(partialId);
        if (partial == null)
        {
            throw new EntityNotFoundException($"Entity of type (Partial) with partial ({partialId}) not found.");
        }
        
        return await daoFactory.subjectPartialDao!.getListByPartialIdAndEnrollmentId(partial.partialId, enrollmentId);
    }

    public async Task<byte[]> getEvaluationStatistics(string teacherId, int partialId, string userId)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var sheetMaker = new SpreadSheetMaker(daoFactory);
        return await sheetMaker.getEvaluationStatisticsByTeacher(enrollmentId, partialId, userId);
    }
}