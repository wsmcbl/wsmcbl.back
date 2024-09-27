using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class DegreeToCreateDto : IBaseDto<DegreeEntity>
{
    [Required] public string label { get; set; } = null!;
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string modality { get; set; } = null!;
    [Required] public List<SubjectInputDto>? subjects { get; set; }

    public DegreeEntity toEntity()
    {
        var degree = new DegreeEntity();

        degree.label = label;
        degree.schoolYear = schoolYear;
        degree.modality = modality;
        
        var list = subjects!.Select(e => e.toEntity()).ToList();
        degree.setSubjectList(list);

        return degree;
    }

    public DegreeToCreateDto()
    {
    }

    public DegreeToCreateDto(DegreeEntity degree)
    {
        label = degree.label;
        modality = degree.modality;
        schoolYear = degree.schoolYear;
        subjects = !degree.subjectList.Any() ? [] : degree.subjectList.mapListToInputDto();
    }
}