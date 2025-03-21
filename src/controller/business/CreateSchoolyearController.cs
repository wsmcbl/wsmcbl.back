using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.controller.business;

public class CreateSchoolyearController : BaseController
{
    public CreateSchoolyearController(DaoFactory daoFactory) : base(daoFactory)
    {
        schoolyear = new SchoolyearEntity();
    }
    
    public async Task<List<SchoolyearEntity>> getSchoolyearList()
    {
        return await daoFactory.schoolyearDao!.getAll();
    }

    private SchoolyearEntity schoolyear { get; set; }
    public SchoolyearEntity getSchoolyearCreated() => schoolyear;
    
    public async Task createSchoolyear(List<PartialEntity> partialList, List<TariffEntity> tariffList)
    {
        await using var contextTransaction = await daoFactory.GetContextTransaction();
        if (contextTransaction == null)
        {
            throw new InternalException("The database is not available to perform this action.");
        }

        try
        {
            schoolyear = await daoFactory.schoolyearDao!.createNewOrFail();
            await createPartialList(partialList);
            await createSubjectList();
            await createTariffList(tariffList);
            await createExchangeRate();

            await contextTransaction.CommitAsync();
        }
        catch (BadHttpRequestException)
        {
            await contextTransaction.RollbackAsync();
            throw;
        }
        catch
        {
            await contextTransaction.RollbackAsync();
            throw new BadHttpRequestException("An error occurred while saving the data, please review the data entered.", 500);
        }
    }

    public async Task<SchoolyearEntity> getSchoolyearById(string schoolyearId)
    {
        var result = await daoFactory.schoolyearDao!.getById(schoolyearId, true);
        if (result == null)
        {
            throw new EntityNotFoundException("SchoolyearEntity", schoolyearId);
        }

        return result;
    }

    private async Task createSubjectList()
    {
        var list = await daoFactory.degreeDataDao!.getAll();
        if (list.Count == 0)
        {
            throw new InternalException("No degreeData available.");
        }
        
        schoolyear.setDegreeDataList(list);
        await daoFactory.degreeDao!.createRange(schoolyear.degreeList!);
    }

    private async Task createTariffList(List<TariffEntity> tariffList)
    {
        if (tariffList.Count == 0)
        {
            throw new IncorrectDataException("TariffList", "The list must be no empty.");
        }

        var tariffsNotValid = tariffList.Where(e => e.amount < 1).ToList().Count;
        if (tariffsNotValid > 0)
        {
            throw new BadRequestException($"{tariffsNotValid} tariffs do not have a valid Amount.");
        }

        schoolyear.setTariffList(tariffList);

        await daoFactory.tariffDao!.createRange(schoolyear.tariffList!);
    }

    private async Task createPartialList(List<PartialEntity> partialList)
    {
        var firstSemester = createSemester(1, partialList);
        var secondSemester = createSemester(2, partialList);
        
        daoFactory.semesterDao!.create(firstSemester);
        daoFactory.semesterDao!.create(secondSemester);
        await daoFactory.ExecuteAsync();

        schoolyear.semesterList = [firstSemester, secondSemester];
    }
    
    private SemesterEntity createSemester(int semester, IEnumerable<PartialEntity> partialList)
    {
        var result = new SemesterEntity
        {
            isActive = false,
            semester = semester,
            schoolyearId = schoolyear.id!,
            partialList = partialList.Where(e => e.semester == semester).ToList()
        };

        result.updateLabel();
        result.updateDeadLine();

        return result;
    }

    private async Task createExchangeRate()
    {
        await schoolyear.createExchangeRate(daoFactory.exchangeRateDao!);
    }

    public async Task<List<ExchangeRateEntity>> getExchangeRateList()
    {
        return await daoFactory.exchangeRateDao!.getAll();
    }

    public async Task updateCurrentExchangeRate(decimal value)
    {
        var currentRate = await daoFactory.exchangeRateDao!.getLastRate();
        currentRate.value = value;

        await daoFactory.ExecuteAsync();
    }
}