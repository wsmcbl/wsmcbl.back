using wsmcbl.src.exception;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory), IResourceController
{
    public async Task<List<(StudentEntity student, string schoolyear, string enrollment)>> getStudentList()
    {
        return await daoFactory.studentDao!.getListWhitSchoolyearAndEnrollment();
    }

    public async Task<string> getMedia(int type, int schoolyear)
    {
        var result = await daoFactory.schoolyearDao!.getSchoolYearByLabel(schoolyear);
        return await daoFactory.mediaDao!.getByTypeAndSchoolyear(type, result.id!);
    }

    public async Task<MediaEntity> updateMedia(MediaEntity media)
    {
        var result = await daoFactory.mediaDao!.getById(media.mediaId);
        if (result == null)
        {
            throw new EntityNotFoundException("Media", media.mediaId.ToString());
        }
        
        result.update(media);
        await daoFactory.execute();

        return result;
    }

    public async Task<MediaEntity> createMedia(MediaEntity media)
    {
        daoFactory.mediaDao!.create(media);
        await daoFactory.execute();
        return media;
    }

    public async Task<List<MediaEntity>> getMediaList()
    {
        return await daoFactory.mediaDao!.getAll();
    }
}