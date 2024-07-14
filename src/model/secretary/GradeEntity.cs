using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public string gradeId { get; set; }

    private List<SubjectEntity>? subjects;

    public void setSubjects(List<SubjectEntity> list)
    {
        subjects = list;
    }
}