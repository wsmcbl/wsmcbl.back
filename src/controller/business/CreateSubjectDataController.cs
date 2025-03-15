using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public class CreateSubjectDataController : BaseController
{
    public CreateSubjectDataController(DaoFactory daoFactory) : base(daoFactory)
    {
    }

    public async Task<SubjectDataEntity> createSubjectData(SubjectDataEntity subject)
    {
        daoFactory.subjectDataDao!.create(subject);
        await daoFactory.execute();
        return subject;
    }

    public async Task<List<SubjectDataEntity>> getSubjectDataList()
    {
        return await daoFactory.subjectDataDao!.getAll();
    }

    public async Task<SubjectDataEntity> updateSubjectData(SubjectDataEntity value)
    {
        var existedEntity = await daoFactory.subjectDataDao!.getById(value.subjectDataId!);
        if (existedEntity == null)
        {
            throw new EntityNotFoundException("SubjectDataEntity", value.subjectDataId.ToString());
        }

        existedEntity.initials = value.initials;
        await daoFactory.execute();
        
        return existedEntity;
    }
}