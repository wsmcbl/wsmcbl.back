using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class DegreeSubjectDto
{
    public string label { get; set; }
    public string modality { get; set; }
    public string tag { get; set; }
    public List<SubjectToCreateDto> subjectList { get; set; }
    
    public DegreeSubjectDto(DegreeEntity degree)
    {
        label = degree.label;
        modality = degree.educationalLevel;
        tag = degree.tag;
        subjectList = degree.subjectList.Count == 0 ? [] : degree.subjectList.mapListToInputDto();
    }
}