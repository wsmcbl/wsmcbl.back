using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.input;

public class SubjectsToUpdateDto : IBaseDto<SubjectEntity>
{
    [Required]
    public int gradeId { get; set; }
    public List<string> subjectIdsList { get; set; } = null!;
    
    public SubjectEntity toEntity()
    {
        throw new NotImplementedException();
    }
}