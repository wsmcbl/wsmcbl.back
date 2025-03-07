using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.service;

public class SpreadSheetMaker
{
    private IStudentDao studentDao {get; set;}
    
    public SpreadSheetMaker(IStudentDao studentDao)
    {
        this.studentDao = studentDao;
    }
    
    public async Task<byte[]> getStudentRegisterInCurrentSchoolyear(string userId)
    {
        var list = await studentDao.getStudentRegisterInCurrentSchoolyear();

        return [];
    }
}