using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentScoreInformationDto
{
    public string studentName { get; set; }
    public string teacherName { get; set; }
    public string enrollment { get; set; }
    public List<PartialInformationDto> partials { get; set; }
    public string solvencyStateMessage { get; set; } = null!;

    public StudentScoreInformationDto(StudentEntity student, TeacherEntity teacher)
    {
        studentName = student.fullName();
        teacherName = teacher.fullName();
        enrollment = teacher.getEnrollmentLabel();

        partials = [];
        foreach (var item in student.partials!)
        {
            partials.Add(new PartialInformationDto(item));
        }
    }

    public void setSolvencyStateMessage(bool isSolvency)
    {
        solvencyStateMessage = isSolvency ? "Estudiante solvente, seleccionar imprimir." :
            "Estudiante no solvente, impresión de boleta inhabilitada.";
    }
}