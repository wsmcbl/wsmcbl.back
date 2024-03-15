namespace wsmcbl.back.model.accounting;

public class StudentEntities
{
    public List<StudentEntity> getStudentList()
    {
        var students = new List<StudentEntity>();

        var b = getStudent("w");
        b.name = "Jordan";
        
        students.Add(getStudent("w"));
        students.Add(b);

        return students;
    }
    
    public StudentEntity getStudent(string studentId)
    {        
        var a = new StudentEntity();
        a.name = "Kenny";
        a.surname = "Tinoco";
        a.enrollment = "9B";
        return a;
    }
}