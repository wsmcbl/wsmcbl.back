using wsmcbl.back.model.entity.accounting;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : ICollectTariffController
{
    private StudentEntities studentEntities;
    
    public CollectTariffController(StudentEntities studentEntities)
    {
        this.studentEntities = studentEntities;
    }
    
    public StudentEntity getStudent(string id)
    {
        return studentEntities.getStudent(id);
    }

    public List<StudentEntity> getStudentsList()
    {
        return studentEntities.getStudentList();
    }

    public void setStudentId(string studentId)
    {
        
    }
}