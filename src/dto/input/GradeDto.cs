using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class GradeDto : IBaseDto<GradeEntity>
{
    [Required] public int gradeId { get; set; }
    [Required] public string? label { get; set; }
    [Required] public string? schoolYear { get; set; }
    [Required] public string? modality { get; set; }
    
    public GradeEntity toEntity()
    {
        var entity = new GradeEntity();
        entity.setGradeId(gradeId);
        entity.init(label!, schoolYear!, modality!);
        return entity;
    }
}