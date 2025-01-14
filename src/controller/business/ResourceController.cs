using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory)
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

    public async Task<List<string>> getTutorList()
    {
        var list = await daoFactory.studentTutorDao!.getAll();

        return list.Where(e => e.isValidPhone())
            .Select(e => e.phone[..8]).Distinct().ToList();
    }
}