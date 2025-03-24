using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.service.sheet;

public class SpreadSheetMaker
{
    private DaoFactory daoFactory { get; set; }

    public SpreadSheetMaker(DaoFactory daoFactory)
    {
        this.daoFactory = daoFactory;
    }

    public async Task<byte[]> getStudentRegisterForCurrentSchoolyear(string userId)
    {
        var user = await daoFactory.userDao!.getById(userId);
        var currentSchoolyear = await daoFactory.schoolyearDao!.getCurrentOrNew();
        var registerList = await daoFactory.studentDao!.getStudentRegisterListForCurrentSchoolyear();
        
        var sheetBuilder = new StudentRegisterSheetBuilder.Builder()
            .withUser(user)
            .withSchoolyear(currentSchoolyear)
            .withRegisterList(registerList)
            .build();

        return sheetBuilder.getSpreadSheet();
    }
}