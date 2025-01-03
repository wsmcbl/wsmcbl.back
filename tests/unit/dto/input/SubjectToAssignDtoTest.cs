using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class SubjectToAssignDtoTest
{
    [Fact]
    public void toEntity_ShouldReturnSubjectEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aSubjectEnrollDto();

        var result = sut.toEntity("enr001");
        
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.enrollmentId));
        Assert.False(string.IsNullOrWhiteSpace(result.subjectId));
        Assert.Null(result.teacherId);
    }
}