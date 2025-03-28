using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.management;

public class SubjectNameDto
{
    public string subjectId { get; set; }
    public string degree { get; set; }
    public string name { get; set; }
    
    public SubjectNameDto(SubjectEntity subject, DegreeEntity degree)
    {
        subjectId = subject.subjectId!;
        this.degree = degree.label;
        name = subject.name;
    }
}