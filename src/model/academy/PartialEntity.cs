using System.Globalization;
using wsmcbl.src.exception;

namespace wsmcbl.src.model.academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public int semesterId { get; set; }
    public int partial { get; set; }
    public int semester { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly deadLine { get; set; }
    public string label { get; set; } = null!;
    
    public bool isActive { get; set; }
    public bool gradeRecordIsActive { get; set; }
    public DateTime? gradeRecordDeadline { get; set; }
    
    public ICollection<SubjectPartialEntity>? subjectPartialList { get; set; }
    
    public bool recordIsActive()
    {
        return isActive && gradeRecordIsActive;
    }
    
    public void updateLabel()
    {
        if (semester == 1)
        {
            label = partial == 1 ? "I Parcial" : "II Parcial";
        }
        else
        {
            label = partial == 1 ? "III Parcial" : "IV Parcial";
        }
    }

    public void setGradeListByStudent(string studentId)
    {
        foreach (var item in subjectPartialList!)
        {
            item.setStudentGrade(studentId);
        }
    }

    public string getPeriodLabel()
    {
        var culture = new CultureInfo("es-ES");
        return $"{startDate.ToString("dd/MMM/yyyy", culture)} - {deadLine.ToString("dd/MMM/yyyy", culture)}";
    }

    public string getSemesterLabel()
    {
        return semester == 1 ? "I Semestre" : "II Semestre";
    }

    public void enableGradeRecording(DateTime deadline)
    {
        if (gradeRecordIsActive)
        {
            throw new ConflictException("The partial record already has the gradeRecordIsActive attribute active.");
        }

        gradeRecordIsActive = true;
        gradeRecordDeadline = deadline;
    }

    public void disableGradeRecording()
    {
        if (!gradeRecordIsActive)
        {
            return;
        }
 
        gradeRecordIsActive = false;
        gradeRecordDeadline = null;
    }
}