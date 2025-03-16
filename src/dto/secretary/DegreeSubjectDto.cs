using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class DegreeSubjectDto
{
    public string label { get; set; }
    public string educationalLevel { get; set; }
    public string tag { get; set; }
    public List<SubjectDto> subjectList { get; set; }
    
    public DegreeSubjectDto(DegreeEntity degree)
    {
        label = degree.label;
        educationalLevel = degree.educationalLevel;
        tag = degree.tag;
        subjectList = degree.subjectList.Count == 0 ? [] : degree.subjectList.mapListToInputDto();
    }
}