using DocumentFormat.OpenXml.Math;
using wsmcbl.src.dto.academy;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class AddGradeToStudentReturningController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<IEnumerable<SubjectPartialByEnrollmentId>> GetSubjectForEnrollmetByPartialId(string enrollmentId, int partialId)
    {
        var subjectPartialDao = daoFactory.subjectPartialDao;
        var entities = await subjectPartialDao!.getListByPartialIdAndEnrollmentId(partialId, enrollmentId);
        var dtos = entities.Select(e => new SubjectPartialByEnrollmentId
        {
            subjectPartialId = e.subjectPartialId,
            subjectId = e.subjectId,
            enrollmentId = e.enrollmentId,
            partialId = e.partialId,
            teacherId = e.teacherId
        }).ToList();

        return dtos;
    }
    
    public async Task<SaveGradeToStudentReturningDto> saveGrades(SaveGradeToStudentReturningDto dto)
    {
        var gradeDao = daoFactory.gradeDao;

        try
        {
            // 1. Obtener todos los ID de subjectPartial que vienen en la petición
            var incomingSubjectPartialIds = dto.grades.Select(g => g.subjectPartialId).ToList();

            // 2. Traer de la base de datos las notas que ya existen para este estudiante
            var existingGrades = await gradeDao!.getExistingGrades(dto.studentId, incomingSubjectPartialIds);

            // Listas para separar lo nuevo de lo que se va a actualizar
            var gradesToInsert = new List<GradeEntity>();

            // 3. Procesar cada nota que viene del DTO
            foreach (var incomingGrade in dto.grades)
            {
                var currentGradeInDb = existingGrades.FirstOrDefault(e => e.subjectPartialId == incomingGrade.subjectPartialId);

                if (currentGradeInDb != null)
                {
                    currentGradeInDb.updateGrades(incomingGrade.grade, incomingGrade.conductgrade);
                }
                else
                {
                    var newGrade = new GradeEntity
                    {
                        studentId = dto.studentId,
                        subjectPartialId = incomingGrade.subjectPartialId
                    };
                    newGrade.updateGrades(incomingGrade.grade, incomingGrade.conductgrade);
                    gradesToInsert.Add(newGrade);
                }
            }

            await gradeDao.createGradesBulk(gradesToInsert);

            return dto;
        }
        catch (Exception)
        {
             SaveGradeToStudentReturningDto defaultResoult = new();
             return defaultResoult;
        }
    }
}