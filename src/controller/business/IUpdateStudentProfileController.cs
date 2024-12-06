using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IUpdateStudentProfileController
{
    public Task updateStudent(StudentEntity student);
}