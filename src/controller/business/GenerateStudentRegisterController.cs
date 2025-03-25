using wsmcbl.src.controller.service.sheet;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class GenerateStudentRegisterController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<StudentRegisterView>> getPaginatedStudentRegisterView(StudentPagedRequest request)
    {
        return await daoFactory.studentDao!.getPaginatedStudentRegisterView(request);
    }

    public async Task<byte[]> getStudentRegisterDocument(string userId)
    {
        var spreadSheetMaker = new SpreadSheetMaker(daoFactory);
        return await spreadSheetMaker.getStudentRegisterForCurrentSchoolyear(userId);
    }
}