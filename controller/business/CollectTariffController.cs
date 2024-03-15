using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public class CollectTariffController : ICollectTariffController
{
    private IStudentDao studentEntities;
    
    public CollectTariffController(IStudentDao studentEntities)
    {
        this.studentEntities = studentEntities;
    }
    
    public StudentEntity getStudent(string id)
    {
        return studentEntities.read(id);
    }

    public Task<List<StudentEntity>> getStudentsList()
    {
        return studentEntities.getAll();
    }

    public void setStudentId(string studentId)
    {
        
    }
}