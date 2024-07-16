using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class GradeToCreateDto : IBaseDto<GradeEntity>
{
    [Required] public string label { get; set; } = null!;
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string modality { get; set; } = null!;
    public List<string> subjects { get; set; } = null!;
    
    public GradeEntity toEntity()
    {
        var entity = new GradeEntity();
        entity.init(label, schoolYear, modality);
        
        return entity;
    }
}