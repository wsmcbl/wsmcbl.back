namespace wsmcbl.src.controller.business;

public class AddingStudentGradesController : IAddingStudentGradesController
{
    public async Task<object?> getEnrollmentListByTeacherId(string teacherId)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> getSubjectList(string teacherId, string enrollmentId)
    {
        throw new NotImplementedException();
    }

    public async Task addGrades(string teacherId, List<string> grades)
    {
        throw new NotImplementedException();
    }
}