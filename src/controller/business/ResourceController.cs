using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ResourceController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<string> getMedia(int type, int schoolyear)
    {
        var result = await daoFactory.schoolyearDao!.getByLabel(schoolyear);
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
}