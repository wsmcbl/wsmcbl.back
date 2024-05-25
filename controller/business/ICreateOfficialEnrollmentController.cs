using wsmcbl.back.model.secretary;

namespace wsmcbl.back.controller.business;

public interface ICreateOfficialEnrollmentController
{
    public Task<List<StudentEntity>> getStudentList();
}