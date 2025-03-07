using wsmcbl.src.controller.service;
using wsmcbl.src.model;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class GenerateStudentRegisterController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<PagedResult<StudentRegisterView>> getStudentRegisterList(StudentPagedRequest request)
    {
        return await daoFactory.studentDao!.getStudentRegisterViewList(request);
    }

    public async Task<byte[]> getStudentRegisterDocument(string userId)
    {
        var spreadSheetMaker = new SpreadSheetMaker(daoFactory.studentDao!);
        return await spreadSheetMaker.getStudentRegisterInCurrentSchoolyear(userId);
    }
}