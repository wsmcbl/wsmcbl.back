namespace wsmcbl.src.controller.business;

public interface IAddingStudentGradesController
{
    public Task<object?> getEnrollmentListByTeacherId(string teacherId);
    public Task<object?> getSubjectList(string teacherId, string enrollmentId);
    public Task addGrades(string teacherId, List<string> grades);
}