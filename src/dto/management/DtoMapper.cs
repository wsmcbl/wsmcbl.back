using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;
using SubjectEntity = wsmcbl.src.model.secretary.SubjectEntity;

namespace wsmcbl.src.dto.management;

public static class DtoMapper
{
    public static List<PartialDto> mapListToDto(this List<PartialEntity> list) =>
        list.Select(e => new PartialDto(e)).ToList();
    
    public static List<TeacherReportDto> mapListToDto(this List<TeacherEntity> list) =>
        list.Select(e => new TeacherReportDto(e)).ToList();
    
    public static List<BasicEnrollmentDto> mapListToDto(this List<EnrollmentEntity> list) =>
        list.Select(e => new BasicEnrollmentDto(e)).ToList();

    public static List<SubjectNameDto> mapListToDto(this List<SubjectEntity> subjectList, List<DegreeEntity> degreeList)
    {
        var result = new List<SubjectNameDto>();

        foreach (var item in subjectList)
        {
            var degree = degreeList.FirstOrDefault(e => e.degreeId == item.degreeId);
            if (degree == null)
            {
                continue;
            }
            
            result.Add(new SubjectNameDto(item, degree));
        }

        return result;
    }
}