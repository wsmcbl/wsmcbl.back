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
    
    public async Task createSchoolyear()
    {
        schoolyear = await daoFactory.schoolyearDao!.getOrCreateNew();
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

    public async Task createSubjectList()
    {
        var list = await daoFactory.degreeDataDao!.getAll();
        if (list.Count == 0)
        {
            throw new BadRequestException("DegreeList are not valid");
        }
        
        schoolyear.setDegreeDataList(list);
        await daoFactory.degreeDao!.createRange(schoolyear.degreeList!);
    }

    public async Task createTariffList(List<TariffEntity> tariffList)
    {
        if (tariffList.Count == 0)
        {
            throw new BadRequestException("TariffList are not valid");
        }

        var tariffsNotValid = tariffList.Where(e => e.amount < 1).ToList().Count;
        if (tariffsNotValid > 0)
        {
            throw new BadRequestException($"{tariffsNotValid} tariffs do not have a valid Amount.");
        }

        schoolyear.setTariffList(tariffList);

        await daoFactory.tariffDao!.createRange(schoolyear.tariffList!);
    }

    public async Task createPartialList(List<PartialEntity> partialList)
    {
        var firstSemester = createSemester(1, partialList);
        var secondSemester = createSemester(2, partialList);
        
        daoFactory.semesterDao!.create(firstSemester);
        daoFactory.semesterDao!.create(secondSemester);
        await daoFactory.execute();

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

    public async Task createExchangeRate()
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

        await daoFactory.execute();
    }
}