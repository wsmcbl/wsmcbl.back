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
        var degree = new DegreeEntity
        {
            label = label,
            schoolYear = schoolYear,
            educationalLevel = modality
        };

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
        modality = degree.educationalLevel;
        schoolYear = degree.schoolYear;
        subjects = degree.subjectList.Count == 0 ? [] : degree.subjectList.mapListToInputDto();
    }
}