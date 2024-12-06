using wsmcbl.src.model.secretary;

namespace wsmcbl.src.controller.business;

public interface IUpdateStudentProfileController
{
    public Task updateStudent(StudentEntity student);
    public Task updateProfilePicture(string studentId, byte[] picture);
    public Task updateStudentDiscount(string studentId, int discountId);
}