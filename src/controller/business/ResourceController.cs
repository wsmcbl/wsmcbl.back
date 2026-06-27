using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task updateSolvencyMonthSetting(int month, string schoolyearId)
    {
        var mediaList = await getMediaList();
        var existingMedia = mediaList.FirstOrDefault(m => m.type == 100 && m.schoolyearId == schoolyearId);

        if (existingMedia == null)
        {
            var newMedia = new MediaEntity
            {
                type = 100,
                schoolyearId = schoolyearId,
                value = month.ToString()
            };
        
            await createMedia(newMedia);
        }
        else
        {
            existingMedia.value = month.ToString();
            await updateMedia(existingMedia);
        }
    }
    
    public async Task<string> getMedia(int type, int schoolyear)
    {
        var result = await daoFactory.schoolyearDao!.getByLabel(schoolyear);
        return await daoFactory.mediaDao!.getByTypeIdAndSchoolyearId(type, result.id!);
    }

    public async Task<MediaEntity> updateMedia(MediaEntity media)
    {
        var result = await daoFactory.mediaDao!.getById(media.mediaId);
        if (result == null)
        {
            throw new EntityNotFoundException("MediaEntity", media.mediaId.ToString());
        }
        
        result.update(media);
        await daoFactory.ExecuteAsync();

        return result;
    }

    public async Task<MediaEntity> createMedia(MediaEntity media)
    {
        daoFactory.mediaDao!.create(media);
        await daoFactory.ExecuteAsync();
        return media;
    }

    public async Task<List<MediaEntity>> getMediaList()
    {
        return await daoFactory.mediaDao!.getAll();
    }

    public async Task<List<TransactionInvoiceView>> getTransactionInvoiceViewList(DateTime from, DateTime to)
    {
        return await daoFactory.transactionDao!.getTransactionInvoiceViewList(from, to);
    }
    
    public async Task<List<string>> getTutorList()
    {
        var list = await daoFactory.studentTutorDao!.getAll();

        return list.Where(e => e.isValidPhone())
            .Select(e => e.phone[..8]).Distinct().ToList();
    }
}