using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory), IResourceController
{
    public async Task<List<StudentEntity>> getStudentList()
    {
        return await daoFactory.studentDao!.getAll();
    }

    public async Task<string> getMedia(int type, int schoolyear)
    {
        var result = await daoFactory.schoolyearDao!.getSchoolYearByLabel(schoolyear);
        return await daoFactory.mediaDao!.getByTypeAndSchoolyear(type, result.id!);
    }
}