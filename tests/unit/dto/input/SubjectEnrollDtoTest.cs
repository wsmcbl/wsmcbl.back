using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class SubjectEnrollDtoTest
{
    [Fact]
    public void toEntity_ShouldReturnSubjectEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aSubjectEnrollDto();

        var result = sut.toEntity("enr001");
        
        Assert.NotNull(result);
        Assert.NotNull(result.enrollmentId);
        Assert.NotNull(result.subjectId);
        Assert.NotNull(result.teacherId);
        Assert.NotEmpty(result.enrollmentId);
        Assert.NotEmpty(result.subjectId);
        Assert.NotEmpty(result.teacherId);
    }
}