using wsmcbl.back.model.dao;
using wsmcbl.back.model.entity.academy;

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

    public List<StudentEntity> getStudentsList()
    {
        return studentEntities.getAll();
    }

    public void setStudentId(string studentId)
    {
        
    }
}