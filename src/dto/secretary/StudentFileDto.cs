using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentFileDto : IBaseDto<StudentFileEntity>
{
    [Required] public string fileId { get; set; } = null!;
    [JsonRequired] public bool transferSheet { get; set; }
    [JsonRequired] public bool birthDocument { get; set; }
    [JsonRequired] public bool parentIdentifier { get; set; }
    [JsonRequired] public bool updatedGradeReport { get; set; }
    [JsonRequired] public bool conductDocument { get; set; }
    [JsonRequired] public bool financialSolvency { get; set; }

    public StudentFileDto()
    {
    }

    public StudentFileDto(StudentFileEntity entity)
    {
        fileId = entity.fileId;
        transferSheet = entity.transferSheet;
        birthDocument = entity.birthDocument;
        parentIdentifier = entity.parentIdentifier;
        updatedGradeReport = entity.updatedGradeReport;
        conductDocument = entity.conductDocument;
        financialSolvency = entity.financialSolvency;
    }
    
    public StudentFileEntity toEntity()
    {
        return new StudentFileEntity
        {
            fileId = fileId,
            transferSheet = transferSheet,
            birthDocument = birthDocument,
            parentIdentifier = parentIdentifier,
            updatedGradeReport = updatedGradeReport,
            conductDocument = conductDocument,
            financialSolvency = financialSolvency
        };
    }
}