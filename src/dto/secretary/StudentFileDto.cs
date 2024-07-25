using Newtonsoft.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class StudentFileDto : IBaseDto<StudentFileEntity>
{
    [JsonRequired] public bool transferSheet { get; set; }
    [JsonRequired] public bool birthDocument { get; set; }
    [JsonRequired] public bool parentIdentifier { get; set; }
    [JsonRequired] public bool updatedGradeReport { get; set; }
    [JsonRequired] public bool conductDocument { get; set; }
    [JsonRequired] public bool financialSolvency { get; set; }
    
    public StudentFileEntity toEntity()
    {
        return new StudentFileEntity
        {
            transferSheet = transferSheet,
            birthDocument = birthDocument,
            parentIdentifier = parentIdentifier,
            updatedGradeReport = updatedGradeReport,
            conductDocument = conductDocument,
            financialSolvency = financialSolvency
        };
    }
}