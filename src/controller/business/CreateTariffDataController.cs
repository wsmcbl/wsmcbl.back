using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateTariffDataController : BaseController
{
    public CreateTariffDataController(DaoFactory daoFactory) : base(daoFactory)
    {
    }
    
    public async Task<List<TariffDataEntity>> getTariffDataList()
    {
        return await daoFactory.tariffDataDao!.getAll();
    }
    
    public async Task updateTariffData(TariffDataEntity value)
    {
        var exitingTariff = await daoFactory.tariffDataDao!.getById(value.tariffDataId);
        if (exitingTariff == null)
        {
            throw new EntityNotFoundException("TariffDataEntity", value.tariffDataId.ToString());
        }
        
        exitingTariff.update(value);
        await daoFactory.ExecuteAsync();
    }

    public async Task<TariffDataEntity> createTariffData(TariffDataEntity tariff)
    {
        daoFactory.tariffDataDao!.create(tariff);
        await daoFactory.ExecuteAsync();
        return tariff;
    }
}