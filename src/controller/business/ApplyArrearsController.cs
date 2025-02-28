using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ApplyArrearsController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public Task<List<TariffEntity>> getOverdueTariffList()
    {
        return daoFactory.tariffDao!.getOverdueList();
    }

    public async Task<TariffEntity> applyArrears(int tariffId)
    {
        if (tariffId <= 0)
        {
            throw new BadRequestException("Invalid ID.");
        }
        
        var tariff = await daoFactory.tariffDao!.getById(tariffId);
        if (tariff is null)
        {
            throw new EntityNotFoundException("Tariff", tariffId.ToString());
        }
        
        tariff.isLate = true;
        
        daoFactory.tariffDao!.update(tariff);
        await daoFactory.execute();
        
        return tariff;
    }
}