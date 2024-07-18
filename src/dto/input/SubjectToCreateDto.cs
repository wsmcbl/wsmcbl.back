using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SubjectToCreateDto : IBaseDto<SubjectDataEntity>
{
    [Required]
    public int gradeId { get; set; }

    public SubjectDataEntity toEntity()
    {
        throw new NotImplementedException();
    }
}