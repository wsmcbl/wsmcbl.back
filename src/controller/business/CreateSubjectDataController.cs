using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateSubjectDataController : BaseController
{
    public CreateSubjectDataController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<List<SubjectDataEntity>> getSubjectDataList()
    {
        return await daoFactory.subjectDataDao!.getAll();
    }

    public async Task<SubjectDataEntity> createSubjectData(SubjectDataEntity subject)
    {
        daoFactory.subjectDataDao!.create(subject);
        await daoFactory.execute();
        return subject;
    }
    
    public async Task updateSubjectData(SubjectDataEntity value)
    {
        var existingEntity = await daoFactory.subjectDataDao!.getById(value.subjectDataId);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException("SubjectDataEntity", value.subjectDataId.ToString());
        }

        existingEntity.update(value);
        await daoFactory.execute();
    }

    public async Task<List<DegreeDataEntity>> getDegreeDataList()
    {
        return await daoFactory.degreeDataDao!.getAll();
    }

    public async Task<List<SubjectAreaEntity>> getSubjectAreaList()
    {
        return await daoFactory.subjectAreaDao!.getAll();
    }

    public async Task updateSubjectArea(int areaId, string name)
    {
        var existingEntity = await daoFactory.subjectAreaDao!.getById(areaId);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException("SubjectAreaEntity", areaId.ToString());
        }

        if (existingEntity.name != name)
        {
            existingEntity.name = name;
            await daoFactory.execute();
        }
    }
}