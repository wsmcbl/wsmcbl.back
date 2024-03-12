namespace wsmcbl.back.model.entity.accounting;

public class StudentEntities
{
    public List<StudentEntity> getStudentList()
    {
        var students = new List<StudentEntity>();

        var b = a();
        b.name = "Jordan";
        
        students.Add(a());
        students.Add(b);

        return students;
    }
    
    public StudentEntity getStudent(string studentId)
    {
        return a();
    }
    
    private StudentEntity a()
    {
        var a = new StudentEntity();
        a.name = "Kenny";
        a.lastName = "Tinoco";
        a.enrollment = "9B";
        return a;
    }
}