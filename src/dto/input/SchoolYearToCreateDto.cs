using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SchoolYearToCreateDto : IBaseDto<GradeEntity>
{
    public GradeEntity toEntity()
    {
        throw new NotImplementedException();
    }

    public List<TariffEntity> getTariffList()
    {
        throw new NotImplementedException();
    }
}