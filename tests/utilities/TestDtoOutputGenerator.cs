using wsmcbl.src.dto.accounting;
using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.utilities;

public class TestDtoOutputGenerator
{
    private static BasicStudentDto aStudentBasicDto()
    {
        var entity = TestEntityGenerator.aAccountingStudent("std-1");
        
        return new BasicStudentDto
        {
            studentId = entity.studentId!,
            fullName = entity.fullName(),
            enrollmentLabel = entity.enrollmentLabel!,
            tutor = entity.tutor!,
        };
    }
    
    public static List<BasicStudentDto> aStudentBasicDtoList()
    {
        return [aStudentBasicDto()];
    }
}
