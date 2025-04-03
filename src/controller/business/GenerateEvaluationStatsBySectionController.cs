using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class GenerateEvaluationStatsBySectionController : BaseController
{
    public GenerateEvaluationStatsBySectionController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<List<StudentEntity>> getStudentListByTeacherId(string teacherId, int partial)
    {
        var controller = new GeneratePerformanceReportBySectionController(daoFactory);
        return await controller.getStudentListByTeacherId(teacherId, partial);
    }

    public async Task<List<SubjectPartialEntity>> getSubjectListByTeacherId(string teacherId, int partial)
    {
        var enrollmentId = await daoFactory.teacherDao!.getCurrentEnrollmentId(teacherId);
        
        var partialList = await daoFactory.partialDao!.getListForCurrentSchoolyear();
        var currentPartial = partialList.FirstOrDefault(e => e.isPartialPosition(partial));
        if (currentPartial == null)
        {
            throw new EntityNotFoundException($"Entity of type (Partial) with partial ({partial}) not found.");
        }
        
        return await daoFactory.subjectPartialDao!.getListByPartialIdAndEnrollmentId(currentPartial.partialId, enrollmentId);
    }
}